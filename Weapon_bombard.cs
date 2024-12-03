using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon_bombard : Weapon_Threeshot
{
    void Awake()
    {
        player = GameManager.Instance.player;
    }


    public override void Fire()
    {

        // 2D 환경에서의 랜덤 위치를 생성
        Vector2 randomDir2D = Random.insideUnitCircle * 5f;//범위
        Vector3 start = new Vector3(randomDir2D.x, randomDir2D.y, 0);
        start += transform.position;

        Vector3 dir = Vector3.down;
        dir = dir.normalized;

        // 총알을 Pool에서 가져와 위치와 방향 설정
        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start; // 총알의 시작 위치 설정
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // 초기화 시 랜덤 방향으로 발사

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // 사운드 재생
    }
}