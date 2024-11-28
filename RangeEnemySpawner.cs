using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemySpawner : Spawnner
{

    protected override void Spawn()
    {   //0~1�� �ش� ������Ʈ ������ ���� ��ȯ
        GameObject RangeEnemy = GameManager.Instance.pool.RangeEnemyGet(0);
        //�ڽ� ������Ʈ������ ���õǵ��� 1���� ����
        RangeEnemy.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        RangeEnemy.GetComponent<RangeEnemy>().Init(spawndata[Level]);
    }
}