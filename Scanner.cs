using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearstTargets;


    void FixedUpdate()
    {
        //1. 캐스팅 시작위치
        //2. 원의 반지름
        //3. 캐스팅 방향
        //4. 캐스팅 길이
        //5. 대상 레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearstTargets = GetNearest();
    }

    Transform GetNearest()//가장 가까운 것을 찾는 함수
    {
        Transform result = null;
        float diff = 100;//최소거리

        foreach(RaycastHit2D target in targets)//해당 반복문을 돌며 가져온 거리가 저장된 거리보다 작을시 교체
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff) //지금 가져온 타켓의 거리 < 최소거리
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;//가장 작은 값 반환
    }

}
