using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCoin : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"일 경우
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.exp += 2;
            // 코인 오브젝트 비활성화 (자기 자신을 비활성화)
            this.gameObject.SetActive(false);
        }
    }
}
