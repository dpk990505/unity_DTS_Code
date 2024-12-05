using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
//기어로직수정완료
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
                GameManager.Instance.player.power += rate;

                break;

            case 1:// 이동속도
                GameManager.Instance.player.speed += rate;

                break;

            case 2://공격속도
                GameManager.Instance.player.fire_rate += rate;

                break;

            case 3://받는피해
                GameManager.Instance.player.damage_taking += rate;

                break;

            case 4://최대체력
                GameManager.Instance.player.max_health += rate;
                GameManager.Instance.player.curr_health += rate;

                break;
            case 5://체력재생
                GameManager.Instance.player.health_regen += rate;

                break;
        }
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
}
