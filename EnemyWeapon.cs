using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int id;//���� ���̵�
    public int prefabId;//������ ���̵�
    public float damage;//������
    public int count;//����
    public float speed;//�ӵ�


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
    void Batch()//������ ���⸦ ��ġ�ϴ� �Լ�
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
        //if (!enemy.scanner.nearstTargets)//�����Ǵ� ��ǥ�� ������
        //    return;
        // ���� ����� �޾Ҵ� = �÷��̾� ��ġ�� �ȴ�
        // ���� �ǹ��̱� ������ ���⿡�� �� �÷��̾��� ��ġ�� ã�� �ൿ�� �ʿ䰡 ����.

        Vector3 targetPos = GameManager.Instance.player.GetComponent<Rigidbody2D>().position;//����� ���� ��ġx �÷��̾��� ��ġo
        Vector3 dir = targetPos - transform.position;//��ǥ��ġ-������ġ
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.EnemyBullet(prefabId).transform;//������ID��Ͽ��� �� ������Ʈ ����
        bullet.position = transform.position;//�ش� ��ǥ ����Ŵ
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//������ ���� �߽����� ��ǥ�� ���� ȸ��
        bullet.GetComponent<EnemeyBullet>().Init(damage, count, dir);

    }
}
