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
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / levelTime),spawndata.Length-1);//��Ʈ�� ��ȯ, ����� ���ӽð�/10 = ���ӷ���

        if (timer > spawndata[Level].spawnTime) {
            timer = 0;
            Spawn();
        }
    }

    protected virtual void Spawn()
    {   //0~1�� �ش� ������Ʈ ������ ���� ��ȯ
        GameObject enemy =  GameManager.Instance.pool.Get(0);
        //�ڽ� ������Ʈ������ ���õǵ��� 1���� ����
        enemy.transform.position = SpawnPoint[Random.Range(1,SpawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawndata[Level]);
    }
}

[System.Serializable]//����ȭ
public class SpawnData
{
    public int spriteType;//��������Ʈ Ÿ��
    public float spawnTime;//��ȯ�ð�
    public int hp;//���� ü��
    public float speed;//���� �ӵ�
}