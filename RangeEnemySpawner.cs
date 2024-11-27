using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemySpawner : Spawnner
{

    protected override void Spawn()
    {   //0~1번 해당 오브젝트 레벨에 따라 소환
        GameObject RangeEnemy = GameManager.Instance.pool.RangeEnemyGet(0);
        //자식 오브젝트에서만 선택되도록 1부터 시작
        RangeEnemy.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        RangeEnemy.GetComponent<RangeEnemy>().Init(spawndata[Level]);
    }
}


[System.Serializable]//직렬화
public class RangeEnymeData
{
    public int spriteType;//스프라이트 타입
    public float spawnTime;//소환시
    public int hp;//몬스터 체력
    public float speed;//몬스터 속도
}