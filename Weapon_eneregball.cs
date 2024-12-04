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
        //초기 설정
        // name = "Weapon " + data.sub_type;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;//지역위치를 플레이어 위치로 변경

        //이후 캐릭터 정보에 따른 설정
        id = data.sub_type;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;
        Life_time = data.Life_time;

        for (int index = 0; index < GameManager.Instance.pool.WeaponPrefabs.Length; index++)
        {
            //프리펩 아이디 찾는 코드, 풀링 매니저의 변수에서 찾아서 초기화
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
