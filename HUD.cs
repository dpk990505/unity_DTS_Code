using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { EXP, Level, Kill, Time,Hp,Coin }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.EXP:
                float curEXP = GameManager.Instance.exp;
                float MaxEXP = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.level,GameManager.Instance.nextExp.Length-1)];
                mySlider.value = curEXP / MaxEXP;

            break;
                
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);
            break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break; 

            case InfoType.Time:
                float remainTIme = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTIme / 60);
                int sec = Mathf.FloorToInt(remainTIme % 60);
                myText.text = string.Format("{0:D2}:{1:D2}",min,sec);
            break;

            case InfoType.Hp:
                float curHp = GameManager.Instance.player.curr_health;
                float MaxHp = GameManager.Instance.player.max_health;
                mySlider.value = curHp / MaxHp;
                break;
                
        }
    }

}
