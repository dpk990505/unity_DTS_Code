using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnner : Spawnner
{

    protected override void Spawn()
    {   //0~1번 해당 오브젝트 레벨에 따라 소환
        GameObject Boss = GameManager.Instance.pool.BossGet(0);
        //자식 오브젝트에서만 선택되도록 1부터 시작
        Boss.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        Boss.GetComponent<Boss>().Init(spawndata[Level]);

    }
}


[System.Serializable]//직렬화
public class BossSpawnData
{
    public int spriteType;//스프라이트 타입
    public float spawnTime;//소환시
    public int hp;//몬스터 체력
    public float speed;//몬스터 속도
}