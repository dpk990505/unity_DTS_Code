using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    //������ ���� ����
    public GameObject[] prefabs;
    public GameObject[] Bossprefabs;
    public GameObject[] RangeEnemyfabs;
    public GameObject [] WeaponPrefabs;
    public GameObject[] EnemyBulletPrefabs;
    public GameObject[] CoinPrefabs;
    public GameObject[] BoxPrefabs;
    //Ǯ ��� ����Ʈ��
    List<GameObject>[] pools;
    List<GameObject>[] Bosspools;
    List<GameObject>[] WeaponPools;
    List<GameObject>[] RangeEnemyPools;
    List<GameObject>[] EnemyBulletPools;
    List<GameObject>[] CoinPools;
    List<GameObject>[] BoxPools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject> ();
        }

        Bosspools = new List<GameObject>[Bossprefabs.Length];

        for (int index = 0; index < Bosspools.Length; index++)
        {
            Bosspools[index] = new List<GameObject>();
        }

        WeaponPools = new List<GameObject>[WeaponPrefabs.Length];

        for (int index = 0; index < WeaponPools.Length; index++)
        {
            WeaponPools[index] = new List<GameObject>();
        }

        RangeEnemyPools =new List<GameObject>[RangeEnemyfabs.Length];

        for(int index = 0; index < RangeEnemyfabs.Length; index++)
        {
            RangeEnemyPools[index] = new List<GameObject>();
        }

        EnemyBulletPools = new List<GameObject>[EnemyBulletPrefabs.Length];

        for (int index = 0; index < EnemyBulletPrefabs.Length; index++)
        {
            EnemyBulletPools[index] = new List<GameObject>();
        }

        CoinPools = new List<GameObject>[CoinPrefabs.Length];

        for (int index = 0; index < CoinPrefabs.Length; index++)
        {
            CoinPools[index] = new List<GameObject>();
        }

        BoxPools = new List<GameObject>[BoxPrefabs.Length];

        for (int index = 0; index < BoxPrefabs.Length; index++)
        {
            BoxPools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����
        
            foreach(GameObject item in pools[index])//pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive (true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if(select == null) {
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }
        return select;
    }

    public GameObject BossGet(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����

        foreach (GameObject item in Bosspools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(Bossprefabs[index], transform);
            Bosspools[index].Add(select);
        }
        return select;
    }

    public GameObject WeaponGet(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����

        foreach (GameObject item in WeaponPools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(WeaponPrefabs[index], transform);
            WeaponPools[index].Add(select);
        }
        return select;
    }

    public GameObject RangeEnemyGet(int index)
    {
        GameObject select = null;
        foreach (GameObject item in RangeEnemyPools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(RangeEnemyfabs[index], transform);
            RangeEnemyPools[index].Add(select);
        }
        return select;
    }

    public GameObject EnemyBullet(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����

        foreach (GameObject item in EnemyBulletPools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(EnemyBulletPrefabs[index], transform);
            EnemyBulletPools[index].Add(select);
        }
        return select;
    }

    public GameObject CoinGet(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����

        foreach (GameObject item in CoinPools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(CoinPrefabs[index], transform);
            CoinPools[index].Add(select);
        }
        return select;
    }

    public GameObject BoxGet(int index)
    {
        GameObject select = null;
        //�ش� Ǯ ������Ʈ ����

        foreach (GameObject item in BoxPools[index])//�ش�pools���� �ȿ� �ִ� �ε��� ���� �˻�
        {
            if (!item.activeSelf)//��Ȱ��ȭ �����Ͻ�
            {   //�߽߰� selset������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������ ���Ӱ� ���� �� 
        if (select == null)
        {
            select = Instantiate(BoxPrefabs[index], transform);
            BoxPools[index].Add(select);
        }
        return select;
    }
}
