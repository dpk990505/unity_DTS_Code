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
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//현제 상태를가져오는 함수
            return;

        float Distance = Vector3.Distance(target.position, rigid.position); // Scanner 에서 뜯어옴.

        if(Distance>= 5)
        {
            Vector2 dirVec = target.position - rigid.position;//위치차이 = 타겟위치 - 나의 위치
            anim.ResetTrigger("Attack");
            //플레이어 키입력 값을 더한 이동 =  몬스터 방향값을 더한 이동
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;//물리적 충돌이 이동속도에 영향을 안가게 설정
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
