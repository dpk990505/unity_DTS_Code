using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_eneregball : Weapon
{

    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        transform.Rotate(Vector3.back * speed * Time.deltaTime);

    }

    public override void Init(ItemData data)
    {
        base.setting(data);

        speed = 150 * GameManager.Instance.player.projectile_speed;

        Batch();

    }

    public override void LevelUp(float damage, int count, float speed,int bust_num)
    {
        base.LevelUp(damage, count, speed,bust_num);
        Batch();
    }

    protected virtual void Batch()//생성된 무기를 배치하는 함수, 기존 Weapon에서 안써서 그냥 끌고옴.
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
