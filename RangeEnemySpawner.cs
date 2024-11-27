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