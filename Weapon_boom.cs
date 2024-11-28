using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_boom : Weapon
{

    public override void Fire()
    {
        Vector3 start = player.scanner.nearstTargets.position;
        Vector3 dir = Vector3.down;//��ǥ��ġ-������ġ
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//������ID��Ͽ��� �� ������Ʈ ����
        bullet.position = start;//�ش� ��ǥ ����Ŵ
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//������ ���� �߽����� ��ǥ�� ���� ȸ��
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir, Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ���
    }

}
