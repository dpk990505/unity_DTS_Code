using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject UiNote;

    enum Achive { Unlock1, Unlock2 };
    Achive[] achives;
    WaitForSecondsRealtime wait;


    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));//�־��� �������� ������ ��� ������
        wait = new WaitForSecondsRealtime(5f);
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()//�ش� ���� Ŭ���� ���� ���� 
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(),0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAhcive(achive);
        }
    }

    void CheckAhcive(Achive achive)//ĳ���� �ر�����
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.Unlock1:
                isAchive = GameManager.Instance.kill >= 10;
                break;
            case Achive.Unlock2:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int i = 0; i < UiNote.transform.childCount; i++)//�ڽ��� �����->chidCount;
            {
                bool isAtcitve = i == (int)achive;
                UiNote.transform.GetChild(i).gameObject.SetActive(isAtcitve);
            }

            StartCoroutine(NoteRoutine());

        }
    }

    IEnumerator NoteRoutine()
    {
       
        UiNote.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;
        
        UiNote.SetActive(false);
       
    }
}
