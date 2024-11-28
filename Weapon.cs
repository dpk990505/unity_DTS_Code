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
    public float Life_time;


    float timer;
    protected Player player;
    Gear gear;

   


    void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        onUpdate();
    }

    public virtual void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;
        if (timer > speed)
        {
            if (!player.scanner.nearstTargets)//�����Ǵ� ��ǥ�� ������
                return;
            timer -= speed;
            

            Fire();
        }
    }

    public virtual void LevelUp(float damage, int count, float speed)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;

    }

    protected virtual void setting(ItemData data)//�����丵�ڵ�
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
    }

    public virtual void Init(ItemData data)//item Data���� ������ �޾ƿ�
    {
        
        setting(data);
        speed = 0.5f * GameManager.Instance.player.fire_rate;
        speed = data.baseSpeed;


        // �� �̻� ���� �������� ��ü �ּ�
        //switch (id)//���� ���̵� ���� ������ �и�(����ü,�÷��̾��ֺ�����)
        //{
        //    //case 0://���� ���̵� 0 �ϰ�� �÷��̾� �ֺ� ����
        //        //speed = 150 * Character.WeaponSpeed;
        //        //Batch();
        //        //break;
        //    default://�������� ����ü ���
        //        speed = 0.5f *Character.WeaponRate;
        //        speed = data.baseSpeed;
        //        break;
        //}

    }

    public virtual void Fire()
    {
        Vector3 start = transform.position;
        Vector3 targetPos = player.scanner.nearstTargets.position;//����� ���� ��ġ
        Vector3 dir = targetPos - transform.position;//��ǥ��ġ-������ġ
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//������ID��Ͽ��� �� ������Ʈ ����
        bullet.position = start;//�ش� ��ǥ ����Ŵ
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//������ ���� �߽����� ��ǥ�� ���� ȸ��
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir,Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ���
    }

}
