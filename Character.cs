using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour//ĳ���� �ʹ� Ư�� ����
{
    public static float Speed//�̼���10%����
    {
        get { return GameManager.Instance.playerId == 0 ? 1.1f : 1f; }
    }

    public static float Damage//���ط�10��
    {
        get { return GameManager.Instance.playerId == 1 ? 1.1f : 1f; }
    }
    public static float WeaponSpeed
    {
        get { return GameManager.Instance.playerId == 2 ? 1.1f : 1f; }
    }

    public static float WeaponRate
    {
        get { return GameManager.Instance.playerId == 3 ? 1.1f : 1f; }
    }



    public static int Count
    {
        get { return GameManager.Instance.playerId == 3 ? 1 : 1; }
    }

}
