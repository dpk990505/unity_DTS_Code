using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float hp;
    public float maxhp;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animCon;
    protected Animator anim;
    protected WaitForFixedUpdate wait;

    protected bool isLive;

    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Collider2D coll;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//현제 상태를가져오는 함수
            return;

        Vector2 dirVec = target.position - rigid.position;//위치차이 = 타겟위치 - 나의 위치

        //플레이어 키입력 값을 더한 이동 =  몬스터 방향값을 더한 이동
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;//물리적 충돌이 이동속도에 영향을 안가게 설정

    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive)
            return;
        sprite.flipX = target.position.x > rigid.position.x;// 목표의 x값보다 크다면 플립X 실행
    }
    void OnEnable()//프리팹의 타깃 초기화 후 플레이어 추적
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;

        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);

        hp = maxhp;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed; ;
        maxhp = data.hp;
        hp = data.hp;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive)
            return;
        if (collision.CompareTag("Bullet"))
        {
            hp -= collision.GetComponent<Bullet>().damage;
            StartCoroutine(KnockBack());


            if (hp > 0)
            {
                anim.SetTrigger("Hit");
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                sprite.sortingOrder = 1;
                anim.SetBool("Dead", true);
                GameManager.Instance.kill++;
                GameManager.Instance.GetExp();

                if (GameManager.Instance.isLive)
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }



    }

    IEnumerator KnockBack()
    {
        yield return wait;//다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);//플레이어 반대 방향으로 3의 힘을 즉발적으로 줌
    }


    void Dead()
    {
        gameObject.SetActive(false);
        DropCoins();
        DropExp();

    }

    private void DropCoins()
    {      
        int r = Random.Range(1, 10);

        if (r > 7) { 
            GameObject coin = GameManager.Instance.pool.CoinGet(0);
            coin.transform.position = transform.position;
            }
    }
    private void DropExp()
    {
        int r = Random.Range(1, 10);

        if (r >= 5)
        {
            GameObject coin = GameManager.Instance.pool.CoinGet(1);
            coin.transform.position = transform.position;
        }
    }
}