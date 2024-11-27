using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Sprite[] sprites;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"�� ���
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.coin += 10;

            // ���� ȹ�� �� PlayerPrefs�� ���� (���� ���� �Ŀ��� �����ǵ���)
            PlayerPrefs.SetInt("CoinCount", GameManager.Instance.coin);
            PlayerPrefs.Save(); // ����� ���� ��ũ�� �ݿ�
            // ���� ������Ʈ ��Ȱ��ȭ (�ڱ� �ڽ��� ��Ȱ��ȭ)
            this.gameObject.SetActive(false);
        }
    }
}

