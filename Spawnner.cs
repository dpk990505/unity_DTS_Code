using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public SpawnData[] spawndata;
    public float levelTime;

    protected float timer;

    protected int Level;


    void Awake()
    {
        SpawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.Instance.maxGameTime/spawndata.Length;
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / levelTime),spawndata.Length-1);//인트로 변환, 진행된 게임시간/10 = 게임레벨

        if (timer > spawndata[Level].spawnTime) {
            timer = 0;
            Spawn();
        }
    }

    protected virtual void Spawn()
    {   //0~1번 해당 오브젝트 레벨에 따라 소환
        GameObject enemy =  GameManager.Instance.pool.Get(0);
        //자식 오브젝트에서만 선택되도록 1부터 시작
        enemy.transform.position = SpawnPoint[Random.Range(1,SpawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawndata[Level]);
    }
}

[System.Serializable]//직렬화
public class SpawnData
{
    public int spriteType;//스프라이트 타입
    public float spawnTime;//소환시간
    public int hp;//몬스터 체력
    public float speed;//몬스터 속도
}