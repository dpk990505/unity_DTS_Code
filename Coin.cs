using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Sprite[] sprites;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"일 경우
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.coin += 10;

            // 코인 획득 후 PlayerPrefs에 저장 (게임 종료 후에도 유지되도록)
            PlayerPrefs.SetInt("CoinCount", GameManager.Instance.coin);
            PlayerPrefs.Save(); // 저장된 값을 디스크에 반영
            // 코인 오브젝트 비활성화 (자기 자신을 비활성화)
            this.gameObject.SetActive(false);
        }
    }
}

