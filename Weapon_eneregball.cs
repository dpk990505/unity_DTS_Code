using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_eneregball : Weapon
{
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    public override void Init(ItemData data)
    {
        //�ʱ� ����
        // name = "Weapon " + data.sub_type;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;//������ġ�� �÷��̾� ��ġ�� ����

        //���� ĳ���� ������ ���� ����
        id = data.sub_type;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;
        Life_time = data.Life_time;

        for (int index = 0; index < GameManager.Instance.pool.WeaponPrefabs.Length; index++)
        {
            //������ ���̵� ã�� �ڵ�, Ǯ�� �Ŵ����� �������� ã�Ƽ� �ʱ�ȭ
            if (data.projectile == GameManager.Instance.pool.WeaponPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        speed = 150 * GameManager.Instance.player.projectile_speed;

        Batch();

    }

    public override void LevelUp(float damage, int count, float speed)
    {
        base.LevelUp(damage, count, speed);
        Batch();
    }

    protected virtual void Batch()//������ ���⸦ ��ġ�ϴ� �Լ�, ���� Weapon���� �ȽἭ �׳� �����.
    {

        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero, -1);

        }
    }
}
