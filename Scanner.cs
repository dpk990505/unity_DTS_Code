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
        //1. ĳ���� ������ġ
        //2. ���� ������
        //3. ĳ���� ����
        //4. ĳ���� ����
        //5. ��� ���̾�
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearstTargets = GetNearest();
    }

    Transform GetNearest()//���� ����� ���� ã�� �Լ�
    {
        Transform result = null;
        float diff = 100;//�ּҰŸ�

        foreach(RaycastHit2D target in targets)//�ش� �ݺ����� ���� ������ �Ÿ��� ����� �Ÿ����� ������ ��ü
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff) //���� ������ Ÿ���� �Ÿ� < �ּҰŸ�
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;//���� ���� �� ��ȯ
    }

}
