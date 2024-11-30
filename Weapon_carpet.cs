﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_carpet : Weapon_front
{
    public override void Fire()
    {
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

        Vector3 start = transform.position - dir * 5;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
        bullet.position = start;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 조이스틱 방향을 향하도록 회전 설정
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // 조이스틱 방향으로 발사 초기화

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // 사운드 재생
    }
}