using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCoin : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"�� ���
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.exp += 2;
            // ���� ������Ʈ ��Ȱ��ȭ (�ڱ� �ڽ��� ��Ȱ��ȭ)
            this.gameObject.SetActive(false);
        }
    }
}
