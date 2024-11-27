using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public float rate;
    public float Movespeed;
    Weapon weapon;

    public void Init(ItemData data, Weapon itemWeapon = null)
    {
        name = "Gear" + data.sub_type;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.sub_type;
        rate = data.damages[0];

        weapon = itemWeapon;

        ApplyGear();
    }

    void ApplyGear()
    {

        switch (type)
        {
            case 0:// 공격력
                Player.power += rate;

                break;

            case 1:// 이동속도
                Player.speed += rate;

                break;

            case 2://공격속도
                Player.fire_rate += rate;

                break;

            case 3://받는피해
                Player.damage_taking += rate;

                break;

            case 4://최대체력
                Player.max_health += rate;
                Player.curr_health += rate;

                break;
            case 5://체력재생
                Player.health_regen += rate;

                break;
        }
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
}
