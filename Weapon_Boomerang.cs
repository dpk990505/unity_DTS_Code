using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Boomerang : Weapon
{
    public override void Fire()
    {
        // 나중에 라이프타임을 정해주던, 직접 정하던 하셈
        Life_time = 2f;
        

        Vector3 start = transform.position;
        Vector3 targetPos = player.scanner.nearstTargets.position;//가까운 적의 위치
        Vector3 dir = targetPos - transform.position;//목표위치-나의위치
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//프리펩ID목록에서 쏠 오브젝트 지정

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;//해당 목표 가리킴
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet_Boomerang>().Init(damage * GameManager.Instance.player.power, count, dir * 4, 4f);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//소리임
    }
}