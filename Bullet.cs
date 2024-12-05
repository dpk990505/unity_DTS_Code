using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;//데미지
    public int pre;//관통계수
    public float speed;//투사체 날라가는 속도
    public Vector3 dir;//투사체 날라가는 속도
    public float Life_time;//유지시간

    protected float timer;

    protected Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;


    void Update()
    {
        if(Life_time >=0)
        {
            timer += Time.deltaTime;
            if (timer >= Life_time)
            {
                gameObject.SetActive(false);//비활성화
                Debug.Log("시간초과 비활성화");
            }

        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float damage, int pre, Vector3 dir, float Life_time)//속도관련 매게변수 dir
    {
        this.damage = damage;
        this.pre = pre;
        this.dir = dir;
        this.Life_time = Life_time;
        timer = 0;
        

        if (pre >=0)
        {
            rigid.velocity = dir * speed; //속력을 곱해 총알이 날라가는 속도
        }
    }

     void OnTriggerEnter2D(Collider2D collision)//몬스터만 관통 카운터 감소
     {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") || collision.CompareTag("RangeEnemy"))
        {
            collision.GetComponent<Enemy>().Taking_Damage(damage * GameManager.Instance.player.power);
            pre--;
        }

        if (pre == -1 )//관통력 숫자가 -1이 되었을시
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
     }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || pre == -100)
            return;

        gameObject.SetActive(false);//비활성화
        
    }

}
