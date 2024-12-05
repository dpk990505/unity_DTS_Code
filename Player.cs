using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    public Vector2 inputVec;
    public Vector2 lastMoveDirection; // 마지막 이동 방향

    // 플레이어 전용 능력치(공격)
    //public float crit_rate = 0.05f; // 캐릭터 치확
    //public float crit_dmg = 1.5f;// 캐릭터 치피
    public float fire_rate = 1f; // 무기 연사력
    public float projectile_speed = 1f; // 투사체 속도
    public float count = 1f;        // 투사체 개수?

    // 플레이어 전용 능력치(생존)
    public float health_mod = 1f;   //체력 배율 (기본체력 * 배율)
    public float health_regen = 0f; // 체력 재생
    public float damage_taking = 1f; // 받는피해
    // public float evade; // 회피

    // 플레이어 전용 능력치(유틸리티)
    public float speed_mod = 1f;    //이동속도 배율 (기본이속 * 배율)
    public float healing_amp = 1f;//캐릭터의 이동 속도 
    public float income_exp = 1f; // 경험치 획득량
    public float income_gold = 1f; // 골드 획득량

    public Scanner scanner;
    public RuntimeAnimatorController[] animCon;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {

        curr_health = max_health;
        switch (GameManager.Instance.playerId)//캐릭터 배열마다 추가로 주어지는 특성
        {
            case 0:
                GameManager.Instance.player.speed += 0.3f;
                break;
            case 1:
                GameManager.Instance.player.power += 0.1f;
                break;
            case 2://이후로 캐릭 아직 없음                
                break;
            case 3:
                fire_rate += 0.1f;
                count += 0f;
                break;

        }

        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    void Update()
    {
        if (inputVec != Vector2.zero)
        {
            lastMoveDirection = inputVec.normalized; // 입력 방향 기록
        }

        if (!GameManager.Instance.isLive)
            return;

        // 체젠하는 부분
        Taking_Heal(health_regen * Time.deltaTime);
    
    }

    void FixedUpdate()//물리 연산 프레임 마다 호출되는 주기함수
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * speed_mod * Time.fixedDeltaTime; 

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

        Taking_Damage(10 * Time.deltaTime);//델타타임당 10식 피해
    }

    public override void Taking_Heal(float amount)
    {
        base.Taking_Heal(amount * healing_amp);
    }
    public override void Taking_Damage(float Amount)
    {
        base.Taking_Damage(Amount * damage_taking); 
    }

    protected override void Got_Dead()
    {
        GameManager.Instance.isLive = false;

        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        anim.SetTrigger("Dead");
        GameManager.Instance.GameOver();
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

}
