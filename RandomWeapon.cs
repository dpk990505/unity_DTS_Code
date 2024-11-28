using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : Weapon
{
    public override void Fire()
    {
        Vector3 start = transform.position;

        // 2D ȯ�濡���� ���� ������ ����
        Vector2 randomDir2D = Random.insideUnitCircle.normalized;
        Vector3 dir = new Vector3(randomDir2D.x, randomDir2D.y, 0);

        // �Ѿ��� Pool���� ������ ��ġ�� ���� ����
        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
        bullet.position = start; // �Ѿ��� ���� ��ġ ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // ���� ������ ���ϵ��� ȸ�� ����
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // �ʱ�ȭ �� ���� �������� �߻�

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // ���� ���
    }

}
