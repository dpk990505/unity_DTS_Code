using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_front : Weapon
{

    public override void Fire()
    {
        Vector3 start = transform.position;
        Vector3 dir = player.inputVec.normalized;

        // dir�� (0, 0)�� ��� ������ �̵� ������ ���
        if (dir == Vector3.zero && player.lastMoveDirection != Vector2.zero)
        {
            dir = player.lastMoveDirection; // ������ �̵� ������ ���
        }
        if (dir == Vector3.zero)
        {
            dir = Vector3.up; // �⺻ �������� ����
        }

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
        bullet.position = start;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // ���̽�ƽ ������ ���ϵ��� ȸ�� ����
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // ���̽�ƽ �������� �߻� �ʱ�ȭ

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // ���� ���
    }
}
