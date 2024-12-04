using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon_front
{
    float timer;

    protected int burst_number = 3;
    protected float burstInterval = 0.2f; // 3점사 간격
    protected float burstCooldown = 1.0f; // 3점사 후 쉬는 시간

    int shotsFired = 0; // 총 발사된 횟수를 추적
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // 발사 주기와 쉬는 시간에 따라 발사
        if (shotsFired < burst_number && timer > burstInterval)
        {
            timer = 0f;
            Fire(); // 발사
            shotsFired++; // 발사 횟수 증가
        }
        else if (shotsFired >= burst_number && timer > burstCooldown)
        {
            // 3점사 후 쉴 때, 발사 횟수 리셋
            shotsFired = 0;
        }
    }
}
