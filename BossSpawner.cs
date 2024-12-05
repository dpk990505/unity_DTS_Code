using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnner : Spawnner
{

    protected override void Spawn()
    {   //0~1�� �ش� ������Ʈ ������ ���� ��ȯ
        GameObject Boss = GameManager.Instance.pool.BossGet(0);
        //�ڽ� ������Ʈ������ ���õǵ��� 1���� ����
        Boss.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        Boss.GetComponent<Boss>().Init(spawndata[0], Level);

    }
}