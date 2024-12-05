using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyBullet : MonoBehaviour
{
    public float damage;//데미지
    public int pre;//관통계수
    public float speed;//투사체 날라가는 속도

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    EnemyWeapon weapon;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    public void Init(float damage, int pre, Vector3 dir)//속도관련 매게변수 dir
    {
        this.damage = damage;
        this.pre = pre;

        if (pre > -1)
        {
            rigid.velocity = dir * speed; //속력을 곱해 총알이 날라가는 속도
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//몬스터만 관통 카운터 감소
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Taking_Damage(damage);
        }

        if (pre <= 0)//관통력 숫자가 -가이 되었을시
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || pre == -100)
            return;

        gameObject.SetActive(false);
    }
}
