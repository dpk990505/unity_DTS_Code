using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public bool isAttacking = false;

    void FixedUpdate()
    {

        if (!GameManager.Instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//���� ���¸��������� �Լ�
            return;

        float Distance = Vector3.Distance(target.position, rigid.position); // Scanner ���� ����.

        if(Distance>= 5)
        {
            Vector2 dirVec = target.position - rigid.position;//��ġ���� = Ÿ����ġ - ���� ��ġ
            anim.ResetTrigger("Attack");
            //�÷��̾� Ű�Է� ���� ���� �̵� =  ���� ���Ⱚ�� ���� �̵�
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;//������ �浹�� �̵��ӵ��� ������ �Ȱ��� ����
            rigid.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            isAttacking = false;

        }
        else
        {
            rigid.velocity = Vector2.zero;
            Attack();
            rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            isAttacking = true;
        }


    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

}
