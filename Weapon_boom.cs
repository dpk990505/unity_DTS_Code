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

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//������ID��Ͽ��� �� ������Ʈ ����

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;//�ش� ��ǥ ����Ŵ
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//������ ���� �߽����� ��ǥ�� ���� ȸ��
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir, Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ���
    }

}
