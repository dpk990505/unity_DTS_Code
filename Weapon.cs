using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;//���� ���̵�
    public int prefabId;//������ ���̵�

    //������ ���� �ɷ�ġ
    public float damage;//������
    public int count;//����
    public float speed;//�ӵ�
    public float Life_time;//����ð�
    


    float timer;
    protected Player player;
    Gear gear;
    protected bool is_rotation_setted = false;
    protected Quaternion base_rotation;

    void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;
        if (timer > speed)
        {
            if (!player.scanner.nearstTargets)//�����Ǵ� ��ǥ�� ������
                return;
            timer = 0f;


            Fire();
        }
    }

    public virtual void LevelUp(float damage, int count, float speed, int bust_num)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;

    }

    public virtual void Init(ItemData data)//item Data���� ������ �޾ƿ�
    {
        //�ʱ� ����
        // name = "Weapon " + data.sub_type;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;//������ġ�� �÷��̾� ��ġ�� ����

        //���� ĳ���� ������ ���� ����
        id = data.sub_type;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;
        Life_time = data.Life_time;

        for (int index = 0; index < GameManager.Instance.pool.WeaponPrefabs.Length; index++)
        {
            //������ ���̵� ã�� �ڵ�, Ǯ�� �Ŵ����� �������� ã�Ƽ� �ʱ�ȭ
            if (data.projectile == GameManager.Instance.pool.WeaponPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        speed = 0.5f * GameManager.Instance.player.fire_rate;
        speed = data.baseSpeed;

    }

    public virtual void Fire()
    {
        Vector3 start = transform.position;
        Vector3 targetPos = player.scanner.nearstTargets.position;//����� ���� ��ġ
        Vector3 dir = targetPos - transform.position;//��ǥ��ġ-������ġ
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//������ID��Ͽ��� �� ������Ʈ ����

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;//�ش� ��ǥ ����Ŵ
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//������ ���� �߽����� ��ǥ�� ���� ȸ��
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir,Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ���
    }

}
