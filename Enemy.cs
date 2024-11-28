using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{

    public Rigidbody2D target;
    public RuntimeAnimatorController[] animCon;
    protected WaitForFixedUpdate wait;

    protected bool isLive;

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

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//���� ���¸��������� �Լ�
            return;

        Vector2 dirVec = target.position - rigid.position;//��ġ���� = Ÿ����ġ - ���� ��ġ

        //�÷��̾� Ű�Է� ���� ���� �̵� =  ���� ���Ⱚ�� ���� �̵�
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;//������ �浹�� �̵��ӵ��� ������ �Ȱ��� ����

    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive)
            return;
        sprite.flipX = target.position.x > rigid.position.x;// ��ǥ�� x������ ũ�ٸ� �ø�X ����
    }
    void OnEnable()//�������� Ÿ�� �ʱ�ȭ �� �÷��̾� ����
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;

        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);

        curr_health = max_health;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        max_health = data.hp;
        curr_health = data.hp;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive)
            return;
        if (collision.CompareTag("Bullet"))
        {
            base.Taking_Damage(collision.GetComponent<Bullet>().damage);
            StartCoroutine(KnockBack());
        }

    }

    IEnumerator KnockBack()
    {
        yield return wait;//���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);//�÷��̾� �ݴ� �������� 3�� ���� ��������� ��
    }

    protected override void Got_Hit()
    {
        anim.SetTrigger("Hit");
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
    }

    protected override void Got_Dead()
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