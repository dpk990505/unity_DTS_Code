using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxSpawnner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    protected float timer;

    public float SpawnTimer;

    protected int Level;

    void Awake()
    {
        SpawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;
        timer += Time.deltaTime;
        if (timer == SpawnTimer)
        {
            timer = 0;
            Spawn();
        }
    }

    protected virtual void Spawn()
    {   //0~1�� �ش� ������Ʈ ������ ���� ��ȯ
        GameObject enemy = GameManager.Instance.pool.Get(0);
        //�ڽ� ������Ʈ������ ���õǵ��� 1���� ����
        enemy.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        enemy.GetComponent<GearBox>();
    }
}
