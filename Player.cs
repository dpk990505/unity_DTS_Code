using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    public Vector2 inputVec;
    public Vector2 lastMoveDirection; // ������ �̵� ����

    // �÷��̾� ���� �ɷ�ġ(����)
    public float crit_rate = 0.05f; // ĳ���� ġȮ
    public float crit_dmg = 1.5f;// ĳ���� ġ��
    public float fire_rate = 1f; // ���� �����
    public float projectile_speed = 1f; // ����ü �ӵ�
    public float count = 1f;        // ����ü ����?

    // �÷��̾� ���� �ɷ�ġ(����)
    public float health_mod = 1f;   //ü�� ���� (�⺻ü�� * ����)
    public float health_regen = 0f; // ü�� ���
    public float damage_taking = 1f; // �޴�����
    // public float evade; // ȸ��

    // �÷��̾� ���� �ɷ�ġ(��ƿ��Ƽ)
    public float speed_mod = 1f;    //�̵��ӵ� ���� (�⺻�̼� * ����)
    public float healing_amp = 1f;//ĳ������ �̵� �ӵ� 
    public float income_exp = 1f; // ����ġ ȹ�淮
    public float income_gold = 1f; // ��� ȹ�淮

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
        speed = 4;
        switch (GameManager.Instance.playerId)
        {
            case 0:
                speed_mod += 0.1f;
                break;
            case 1:
                power += 0.1f;
                break;
            case 2:
                projectile_speed += 0.1f;
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
            lastMoveDirection = inputVec.normalized; // �Է� ���� ���
        }

        if (!GameManager.Instance.isLive)
            return;

        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");

        // ü���ϴ� �κ�
        Taking_Heal(health_regen * Time.deltaTime);
    
    }

    void FixedUpdate()//���� ���� ������ ���� ȣ��Ǵ� �ֱ��Լ�
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * speed_mod * Time.fixedDeltaTime; 

        rigid.MovePosition(rigid.position + nextVec);//�Է¹��� Vec�Լ��� �̵�
    }

    void LateUpdate()//������ ������ �����Լ�
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

        Taking_Damage(10 * Time.deltaTime);//��ŸŸ�Ӵ� 10�� ����
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
