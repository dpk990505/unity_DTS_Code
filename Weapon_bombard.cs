using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon_bombard : Weapon_Threeshot
{
    void Awake()
    {
        player = GameManager.Instance.player;
        burst_number = 5;
    }


    public override void Fire()
    {

        // 2D 환경에서의 랜덤 위치를 생성
        Vector2 randomDir2D = Random.insideUnitCircle * 5f;
        Vector3 start = new Vector3(randomDir2D.x, randomDir2D.y, 0);
        start += transform.position;

        Vector3 dir = Vector3.down;
        dir = dir.normalized;

        // 총알을 Pool에서 가져와 위치와 방향 설정
        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
        bullet.position = start; // 총알의 시작 위치 설정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 랜덤 방향을 향하도록 회전 설정
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // 초기화 시 랜덤 방향으로 발사

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // 사운드 재생
    }
}