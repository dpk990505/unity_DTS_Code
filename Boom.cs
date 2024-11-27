using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float damage;//데미지
    public int pre;//관통계수
    public float speed;//투사체 날라가는 속도

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
       
    }

     void OnTriggerEnter2D(Collider2D collision)//몬스터만 관통 카운터 감소
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") ||collision.CompareTag("RangeEnemy"))
        pre--;

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

        gameObject.SetActive(false);
    }

}
