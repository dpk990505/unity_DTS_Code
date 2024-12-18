using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon_front//멀티샷 코드
{
    float timer;
    public int bust_num;//생성 갯수
    protected float burstInterval = 0.2f; // 3점사 간격
    protected float burstCooldown = 1.0f; // 3점사 후 쉬는 시간

    int shotsFired = 0; // 총 발사된 횟수를 추적

    public override void Init(ItemData data)//리펙토링코드
    {
        base.Init(data);
        bust_num = data.base_bust_num;//멀티샷 쓰는 코드 값 전용
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // 발사 주기와 쉬는 시간에 따라 발사
        if (shotsFired < bust_num && timer > burstInterval)
        {
            timer = 0f;
            Fire(); // 발사
            shotsFired++; // 발사 횟수 증가
        }
        else if (shotsFired >= bust_num && timer > burstCooldown)
        {
            // 3점사 후 쉴 때, 발사 횟수 리셋
            shotsFired = 0;
        }
    }

        public override void LevelUp(float damage, int count, float speed, int bust_num)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;
        this.bust_num += bust_num;
    }
}

