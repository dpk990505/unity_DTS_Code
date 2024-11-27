using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int Level;
    public Weapon weapon;
    public Gear Gear;

    Image icon;
    Text TextLevel;
    Text textName;
    Text textInfo;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.ItemIcon; //������ �������� �����۾����� ������ ������

        Text[] texts = GetComponentsInChildren<Text>();
        TextLevel = texts[0];
        textName = texts[1];
        textInfo = texts[2];
        textName.text = data.itemName;
    }
    private void OnEnable()
    {
        TextLevel.text = "Lv." + (Level + 1);


        if (data.itemType == ItemData.ItemType.Weapon)
        {
            switch (data.sub_type)
            {//*100�� %�� �����ٶ�

                case 0:
                case 1:
                    textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100, data.counts[Level], data.speeds[Level]);
                    break;
                case 2:
                    textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100, data.speeds[Level]);
                    break;
                case 3:
                    textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100, data.counts[Level], data.speeds[Level]);
                    break;
                case 4:
                    textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100, data.counts[Level], data.speeds[Level]);
                    break;
                case 5:
                    textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100, data.counts[Level], data.speeds[Level]);
                    break;
            }
        }
        else if(data.itemType == ItemData.ItemType.Attribute)
        {
            textInfo.text = string.Format(data.itemDesc, data.damages[Level] * 100);
        }
        else
        {
            textInfo.text = string.Format(data.itemDesc);
        }

    }
    public void OnClick()
    {
        if (data.itemType == ItemData.ItemType.Weapon)
        {
            if (Level == 0)
            {
                GameObject newWeapon = new GameObject();
                switch (data.sub_type)
                {
                    case 0:
                        weapon = newWeapon.AddComponent<Weapon_eneregball>();
                        break;
                    case 1:
                        weapon = newWeapon.AddComponent<Weapon>();
                        break;
                    case 2:
                        weapon = newWeapon.AddComponent<Weapon_boom>();
                        break;
                    case 3:
                        weapon = newWeapon.AddComponent<RandomWeapon>();
                        break;
                    case 4:
                        weapon = newWeapon.AddComponent<Weapon_front>();
                        break;
                    case 5:
                        weapon = newWeapon.AddComponent<Weapon_Threeshot>();
                        break;
                }
                weapon.Init(data);
            }
            else
            {
                float nextDamage = data.baseDamage;
                int nextCount = 0;
                float nextSpeed = data.baseSpeed;

                nextDamage += data.baseDamage * data.damages[Level];
                nextCount += data.counts[Level];//�ش� ������ ���� ī��Ʈ�� �޶���
                nextSpeed = data.speeds[Level];//������ ���� ���ǵ�����(ź��, ȸ���ӵ���)

                weapon.LevelUp(nextDamage, nextCount, nextSpeed);
            }

            Level++;
        }
        else if (data.itemType == ItemData.ItemType.Attribute)
        {
            if (Level == 0)
            {
                GameObject newGear = new GameObject();
                Gear = newGear.AddComponent<Gear>();
                Gear.Init(data);
            }
            else
            {
                float GaearDamageUp = data.damages[Level];
                Gear.LevelUp(GaearDamageUp);
            }
            Level++;

        }
        else if (data.itemType == ItemData.ItemType.Heal)
        {
            Player.curr_health = Player.max_health; 

        }
        if (Level == data.damages.Length)//�ִ� ���� �޼���
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
