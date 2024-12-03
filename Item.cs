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
        icon.sprite = data.ItemIcon; //아이템 데이터의 아이템아이콘 정보를 가져옴

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
            {//*100은 %로 보여줄때

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
                case 6:
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
    public virtual void OnClick()
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
                    case 6:
                        weapon = newWeapon.AddComponent<Weapon_bombard>();
                        break;
                    case 7:
                        weapon = newWeapon.AddComponent<Weapon_carpet>();
                        break;
                    case 8:
                        weapon = newWeapon.AddComponent<Weapon_Boomerang>();
                        break;
                }
                weapon.Init(data);
            }
            else
            {
                float nextDamage = data.baseDamage;
                int nextCount = 0;
                float nextSpeed = data.baseSpeed;
                int next_bust_count = data.base_bust_num;

                nextDamage += data.baseDamage * data.damages[Level];
                nextCount += data.counts[Level];//해당 레벨에 따라 카운트가 달라짐
                nextSpeed = data.speeds[Level];//레벨에 따라 스피드증가(탄속, 회전속도등)

                //임시 코드, 이거보다 더 최적화 가능하면 해[줘]
                if (data.bust_numcount != null && Level < data.bust_numcount.Length)
                {
                    next_bust_count += data.bust_numcount[Level]; // 값이 존재할 경우에만 동작
                }
                weapon.LevelUp(nextDamage, nextCount, nextSpeed,next_bust_count);
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
            GameManager.Instance.player.curr_health = GameManager.Instance.player.max_health; 

        }
        if (Level == data.damages.Length)//최대 레벨 달성시
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
