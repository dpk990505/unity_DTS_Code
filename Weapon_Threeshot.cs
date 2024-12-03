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

    protected override void setting(ItemData data)//리펙토링코드
    {
        //초기 설정
        // name = "Weapon " + data.sub_type;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;//지역위치를 플레이어 위치로 변경

        //이후 캐릭터 정보에 따른 설정
        id = data.sub_type;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;
        Life_time = data.Life_time;
        bust_num = data.base_bust_num;//멀티샷 쓰는 코드 값 전용

        for (int index = 0; index < GameManager.Instance.pool.WeaponPrefabs.Length; index++)
        {
            //프리펩 아이디 찾는 코드, 풀링 매니저의 변수에서 찾아서 초기화
            if (data.projectile == GameManager.Instance.pool.WeaponPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }
    }
    public override void onUpdate()
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

