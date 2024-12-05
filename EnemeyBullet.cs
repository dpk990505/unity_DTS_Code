using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyBullet : MonoBehaviour
{
    public float damage;//������
    public int pre;//������
    public float speed;//����ü ���󰡴� �ӵ�

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    EnemyWeapon weapon;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    public void Init(float damage, int pre, Vector3 dir)//�ӵ����� �ŰԺ��� dir
    {
        this.damage = damage;
        this.pre = pre;

        if (pre > -1)
        {
            rigid.velocity = dir * speed; //�ӷ��� ���� �Ѿ��� ���󰡴� �ӵ�
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//���͸� ���� ī���� ����
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Taking_Damage(damage);
        }

        if (pre <= 0)//����� ���ڰ� -���� �Ǿ�����
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
