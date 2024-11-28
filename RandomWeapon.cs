using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : Weapon
{
    public override void Fire()
    {
        Vector3 start = transform.position;

        // 2D 환경에서의 랜덤 방향을 생성
        Vector2 randomDir2D = Random.insideUnitCircle.normalized;
        Vector3 dir = new Vector3(randomDir2D.x, randomDir2D.y, 0);

        // 총알을 Pool에서 가져와 위치와 방향 설정
        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;
        bullet.position = start; // 총알의 시작 위치 설정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 랜덤 방향을 향하도록 회전 설정
        bullet.GetComponent<Bullet>().Init(damage, count, dir, Life_time); // 초기화 시 랜덤 방향으로 발사

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range); // 사운드 재생
    }

}
