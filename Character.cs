using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour//캐릭터 초반 특성 설정
{
    // 모든 캐릭터 (플레이어, 적) 은 이 코드를 상속받음
    // 모든 캐릭터가 가져야 할 사항
    // 컴포넌트 : 애니메이터, 스프라이트, 릿지드 바디
    // 속성 : 최대/현재 체력, 공격력, 이동속도
    // 함수 : 이동, 회복받음, 피해 받음, 피격 반응, 사망 반응

    // 기존에 플레이어가 가질 수 있는 캐릭터의 능력치 부분은 Player.cs 에서 관리함

    // 컴포넌트 선언부
    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Animator anim;

    // 속성 선언부
    public float max_health = 100f; // 최대 체력
    public float curr_health = 100f; // 현재 체력
    public float power = 1f; // 캐릭터 공격력
    public float speed = 4;//캐릭터의 이속


    // 함수 선언부

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
