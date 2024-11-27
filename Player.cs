using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public Vector2 lastMoveDirection; // ������ �̵� ����

    // �÷��̾� �ɷ�ġ(����)
    public static float power = 1f; // ĳ���� ���ݷ�
    public static float crit_rate = 0.05f; // ĳ���� ġȮ
    public static float crit_dmg = 1.5f;// ĳ���� ġ��
    public static float fire_rate = 1f; // ���� �����

    // �÷��̾� �ɷ�ġ(����)
    public static float max_health = 100f; // �ִ� ü��
    public static float curr_health = 100f; // ���� ü��
    public static float health_regen = 0f; // ü�� ���
    public static float damage_taking = 1f; // �޴�����
    // public float evade; // ȸ��

    // �÷��̾� �ɷ�ġ(��ƿ��Ƽ)
    public static float speed = 1f;//ĳ������ �̼�
    public static float healing_amp = 1f;//ĳ������ �̵� �ӵ� 
    public static float income_exp = 1f; // ����ġ ȹ�淮
    public static float income_gold = 1f; // ��� ȹ�淮

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
        speed *= Character.Speed;//ĳ���� ���� �ӵ� ����
        anim.runtimeAnimatorController = aniCon[GameManager.Instance.playerId];
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
        takeHealing(health_regen * Time.deltaTime);
    
    }

    void FixedUpdate()//���� ���� ������ ���� ȣ��Ǵ� �ֱ��Լ�
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime * 4; 

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

        takeDamage(10 * Time.deltaTime);
    }

    public void takeHealing(float amount)
    {
        Player.curr_health += amount * healing_amp;
    }
    public void takeDamage(float amount)
    {
        Player.curr_health -= amount * damage_taking;//��ŸŸ�Ӵ� 10�� ����

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
