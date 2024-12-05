using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;//������
    public int pre;//������
    public float speed;//����ü ���󰡴� �ӵ�
    public Vector3 dir;//����ü ���󰡴� �ӵ�
    public float Life_time;//�����ð�

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
                gameObject.SetActive(false);//��Ȱ��ȭ
                Debug.Log("�ð��ʰ� ��Ȱ��ȭ");
            }

        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float damage, int pre, Vector3 dir, float Life_time)//�ӵ����� �ŰԺ��� dir
    {
        this.damage = damage;
        this.pre = pre;
        this.dir = dir;
        this.Life_time = Life_time;
        timer = 0;
        

        if (pre >=0)
        {
            rigid.velocity = dir * speed; //�ӷ��� ���� �Ѿ��� ���󰡴� �ӵ�
        }
    }

     void OnTriggerEnter2D(Collider2D collision)//���͸� ���� ī���� ����
     {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") || collision.CompareTag("RangeEnemy"))
        {
            collision.GetComponent<Enemy>().Taking_Damage(damage * GameManager.Instance.player.power);
            pre--;
        }

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

        gameObject.SetActive(false);//��Ȱ��ȭ
        
    }

}
