using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour//ĳ���� �ʹ� Ư�� ����
{
    // ��� ĳ���� (�÷��̾�, ��) �� �� �ڵ带 ��ӹ���
    // ��� ĳ���Ͱ� ������ �� ����
    // ������Ʈ : �ִϸ�����, ��������Ʈ, ������ �ٵ�
    // �Ӽ� : �ִ�/���� ü��, ���ݷ�, �̵��ӵ�
    // �Լ� : �̵�, ȸ������, ���� ����, �ǰ� ����, ��� ����

    // ������ �÷��̾ ���� �� �ִ� ĳ������ �ɷ�ġ �κ��� Player.cs ���� ������

    // ������Ʈ �����
    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Animator anim;

    // �Ӽ� �����
    public float max_health = 100f; // �ִ� ü��
    public float curr_health = 100f; // ���� ü��
    public float power = 1f; // ĳ���� ���ݷ�
    public float speed = 4;//ĳ������ �̼�


    // �Լ� �����

    protected virtual void Move(Vector2 dirVec)
    {
        if (!GameManager.Instance.isLive)
            return;

        rigid.MovePosition(rigid.position + dirVec.normalized * speed * Time.fixedDeltaTime);
    }

    public virtual void Taking_Damage(float Amount)
    {
        curr_health -= Amount;
        if(curr_health > 0)
        {
            Got_Hit();
        }
        else
        {
            Got_Dead();
        }
    }
    public virtual void Taking_Heal(float Amount)
    {
        curr_health += Amount;
        if (curr_health > max_health)
        {
            curr_health = max_health;
        }
    }

    protected virtual void Got_Hit()
    {

    }

    protected virtual void Got_Dead()
    {

    }

}
