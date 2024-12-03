using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_front : Weapon
{

    public override void Fire()
    {
        Vector3 start = transform.position;
        Vector3 dir = player.inputVec.normalized;

        // dir이 (0, 0)일 경우 마지막 이동 방향을 사용
        if (dir == Vector3.zero && player.lastMoveDirection != Vector2.zero)
        {
            dir = player.lastMoveDirection; // 마지막 이동 방향을 사용
        }
        if (dir == Vector3.zero)
        {
            dir = Vector3.up; // 기본 방향으로 설정
        }

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // 조이스틱 방향으로 발사 초기화

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // 사운드 재생
    }
}
