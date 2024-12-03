using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_boom : Weapon
{

    public override void Fire()
    {
        Vector3 start = player.scanner.nearstTargets.position;
        Vector3 dir = Vector3.down;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//프리펩ID목록에서 쏠 오브젝트 지정

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;//해당 목표 가리킴
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir, Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//소리임
    }

}
