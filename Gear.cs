using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
//�����������Ϸ�
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
            case 0:// ���ݷ�
                GameManager.Instance.player.power += rate;

                break;

            case 1:// �̵��ӵ�
                GameManager.Instance.player.speed += rate;

                break;

            case 2://���ݼӵ�
                GameManager.Instance.player.fire_rate += rate;

                break;

            case 3://�޴�����
                GameManager.Instance.player.damage_taking += rate;

                break;

            case 4://�ִ�ü��
                GameManager.Instance.player.max_health += rate;
                GameManager.Instance.player.curr_health += rate;

                break;
            case 5://ü�����
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
