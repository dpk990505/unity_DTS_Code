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
    {   //0~1번 해당 오브젝트 레벨에 따라 소환
        GameObject enemy = GameManager.Instance.pool.Get(0);
        //자식 오브젝트에서만 선택되도록 1부터 시작
        enemy.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        enemy.GetComponent<GearBox>();
    }
}
