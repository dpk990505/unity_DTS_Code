using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon
{
    float timer;
    int shotsFired = 0; // �� �߻�� Ƚ���� ����
    public float burstInterval = 0.2f; // 3���� ����
    public float burstCooldown = 1.0f; // 3���� �� ���� �ð�

    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // �߻� �ֱ�� ���� �ð��� ���� �߻�
        if (shotsFired < 3 && timer > burstInterval)
        {
            timer = 0f;
            Vector3 dir = player.inputVec.normalized;

            // dir�� (0, 0)�� ��� ������ �̵� ������ ���
            if (dir == Vector3.zero && player.lastMoveDirection != Vector2.zero)
            {
                dir = player.lastMoveDirection; // ������ �̵� ������ ���
            }

            Fire(transform.position, dir); // �߻�
            shotsFired++; // �߻� Ƚ�� ����
        }
        else if (shotsFired >= 3 && timer > burstCooldown)
        {
            // 3���� �� �� ��, �߻� Ƚ�� ����
            shotsFired = 0;
        }
    }

    public override void Fire(Vector3 start, Vector3 dir)
    {
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
