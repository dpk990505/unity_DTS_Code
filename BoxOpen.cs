using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BoxOpen : MonoBehaviour
{
    RectTransform rect;
    public BoxItem[] items;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<BoxItem>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.Instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.select);
        AudioManager.Instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 모든 아이템 비활성화
        foreach (BoxItem item in items)
        {
            item.gameObject.SetActive(false);
        }

        List<BoxItem> levelableItems = new List<BoxItem>();

        // 레벨업이 가능한 아이템을 선택
        foreach (BoxItem item in items)
        {
            if (item.Level < item.data.damages.Length)
            {
                levelableItems.Add(item);
            }
        }

        // 레벨업 가능한 아이템 중 랜덤으로 3개 선택
        int[] ran = new int[3];
        for (int i = 0; i < 3; i++)
        {
            if (levelableItems.Count > 0)
            {
                // 레벨업 가능한 아이템이 있으면 그 중 하나를 랜덤으로 활성화
                int randomIndex = Random.Range(0, levelableItems.Count);
                levelableItems[randomIndex].gameObject.SetActive(true);
                levelableItems.RemoveAt(randomIndex); // 이미 선택된 아이템은 제거
            }
            else
            {
                // 만약 레벨업 가능한 아이템이 없다면 소비 아이템 활성화
                items[0].gameObject.SetActive(true);  // 소비 아이템 (items[4]) 활성화
            }
        }
    }
}
