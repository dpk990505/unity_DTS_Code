using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour//캐릭터 초반 특성 설정
{
    public static float Speed//이속이10%빠름
    {
        get { return GameManager.Instance.playerId == 0 ? 1.1f : 1f; }
    }

    public static float Damage//피해량10퍼
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
