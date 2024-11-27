using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int id;//무기 아이디
    public int prefabId;//프리펩 아이디
    public float damage;//데미지
    public int count;//갯수
    public float speed;//속도


    float timer;
    RangeEnemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<RangeEnemy>();
    }

    void Start()
    {
        //Init();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (enemy.isAttacking)
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
            timer += Time.deltaTime;

            if (timer > speed)
            {
                timer = 0f;
                Fire();
            }
        }
        else
        {
            timer = 0f;
        }
        
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

    }

    public void Init()
    {
    //    speed = 3f;
    }
    void Batch()//생성된 무기를 배치하는 함수
    {

        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.EnemyBullet(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<EnemeyBullet>().Init(damage, -100, Vector3.zero);

        }
    }

    void Fire()
    {
        //if (!enemy.scanner.nearstTargets)//지정되는 목표가 없을시
        //    return;
        // 공격 명령을 받았다 = 플레이어 위치를 안다
        // 같은 의미이기 때문에 여기에서 또 플레이어의 위치를 찾는 행동이 필요가 없음.

        Vector3 targetPos = GameManager.Instance.player.GetComponent<Rigidbody2D>().position;//가까운 적의 위치x 플레이어의 위치o
        Vector3 dir = targetPos - transform.position;//목표위치-나의위치
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.EnemyBullet(prefabId).transform;//프리펩ID목록에서 쏠 오브젝트 지정
        bullet.position = transform.position;//해당 목표 가리킴
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<EnemeyBullet>().Init(damage, count, dir);

    }
}
