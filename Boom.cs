using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float damage;//������
    public int pre;//������
    public float speed;//����ü ���󰡴� �ӵ�

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
       
    }

     void OnTriggerEnter2D(Collider2D collision)//���͸� ���� ī���� ����
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") ||collision.CompareTag("RangeEnemy"))
        pre--;

        if (pre == -1 )//����� ���ڰ� -1�� �Ǿ�����
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
