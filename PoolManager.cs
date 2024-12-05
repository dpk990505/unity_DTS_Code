using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    //프리팹 보관 변수
    public GameObject[] prefabs;
    public GameObject[] Bossprefabs;
    public GameObject[] RangeEnemyfabs;
    public GameObject [] WeaponPrefabs;
    public GameObject[] EnemyBulletPrefabs;
    public GameObject[] CoinPrefabs;
    public GameObject[] BoxPrefabs;
    //풀 당담 리스트들
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
        //해당 풀 오브젝트 접근
        
            foreach(GameObject item in pools[index])//pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive (true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
        if(select == null) {
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }
        return select;
    }

    public GameObject BossGet(int index)
    {
        GameObject select = null;
        //해당 풀 오브젝트 접근

        foreach (GameObject item in Bosspools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
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
        //해당 풀 오브젝트 접근

        foreach (GameObject item in WeaponPools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
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
        foreach (GameObject item in RangeEnemyPools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
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
        //해당 풀 오브젝트 접근

        foreach (GameObject item in EnemyBulletPools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
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
        //해당 풀 오브젝트 접근

        foreach (GameObject item in CoinPools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
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
        //해당 풀 오브젝트 접근

        foreach (GameObject item in BoxPools[index])//해당pools변수 안에 있는 인덱스 전부 검색
        {
            if (!item.activeSelf)//비활성화 상태일시
            {   //발견시 selset변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았을시 새롭게 생성 후 
        if (select == null)
        {
            select = Instantiate(BoxPrefabs[index], transform);
            BoxPools[index].Add(select);
        }
        return select;
    }
}
