using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public Vector2 lastMoveDirection; // 마지막 이동 방향

    // 플레이어 능력치(공격)
    public static float power = 1f; // 캐릭터 공격력
    public static float crit_rate = 0.05f; // 캐릭터 치확
    public static float crit_dmg = 1.5f;// 캐릭터 치피
    public static float fire_rate = 1f; // 무기 연사력

    // 플레이어 능력치(생존)
    public static float max_health = 100f; // 최대 체력
    public static float curr_health = 100f; // 현재 체력
    public static float health_regen = 0f; // 체력 재생
    public static float damage_taking = 1f; // 받는피해
    // public float evade; // 회피

    // 플레이어 능력치(유틸리티)
    public static float speed = 1f;//캐릭터의 이속
    public static float healing_amp = 1f;//캐릭터의 이동 속도 
    public static float income_exp = 1f; // 경험치 획득량
    public static float income_gold = 1f; // 골드 획득량

    public Scanner scanner;
    public RuntimeAnimatorController[] aniCon;
    
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public Animator anim;

    


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        speed *= Character.Speed;//캐릭터 마다 속도 설정
        anim.runtimeAnimatorController = aniCon[GameManager.Instance.playerId];
    }

    void Update()
    {
        if (inputVec != Vector2.zero)
        {
            lastMoveDirection = inputVec.normalized; // 입력 방향 기록
        }

        if (!GameManager.Instance.isLive)
            return;

        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");

        // 체젠하는 부분
        takeHealing(health_regen * Time.deltaTime);
    
    }

    void FixedUpdate()//물리 연산 프레임 마다 호출되는 주기함수
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime * 4; 

        rigid.MovePosition(rigid.position + nextVec);//입력받은 Vec함수로 이동
    }

    void LateUpdate()//프레임 종료전 실행함수
    {
        if (!GameManager.Instance.isLive)
            return;

        anim.SetFloat("Speed",inputVec.magnitude);

        if(inputVec.x != 0)
        {
            sprite.flipX = inputVec.x>0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive)
            return;

        takeDamage(10 * Time.deltaTime);
    }

    public void takeHealing(float amount)
    {
        Player.curr_health += amount * healing_amp;
    }
    public void takeDamage(float amount)
    {
        Player.curr_health -= amount * damage_taking;//델타타임당 10식 피해

        if(Player.curr_health<= 0)
        {
            GameManager.Instance.isLive = false;

            for (int i =2; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

}
