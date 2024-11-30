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
        // ��� ������ ��Ȱ��ȭ
        foreach (BoxItem item in items)
        {
            item.gameObject.SetActive(false);
        }

        List<BoxItem> levelableItems = new List<BoxItem>();

        // �������� ������ �������� ����
        foreach (BoxItem item in items)
        {
            if (item.Level < item.data.damages.Length)
            {
                levelableItems.Add(item);
            }
        }

        // ������ ������ ������ �� �������� 3�� ����
        int[] ran = new int[3];
        for (int i = 0; i < 3; i++)
        {
            if (levelableItems.Count > 0)
            {
                // ������ ������ �������� ������ �� �� �ϳ��� �������� Ȱ��ȭ
                int randomIndex = Random.Range(0, levelableItems.Count);
                levelableItems[randomIndex].gameObject.SetActive(true);
                levelableItems.RemoveAt(randomIndex); // �̹� ���õ� �������� ����
            }
            else
            {
                // ���� ������ ������ �������� ���ٸ� �Һ� ������ Ȱ��ȭ
                items[0].gameObject.SetActive(true);  // �Һ� ������ (items[4]) Ȱ��ȭ
            }
        }
    }
}
