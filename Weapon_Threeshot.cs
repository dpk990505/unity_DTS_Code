using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon_front//��Ƽ�� �ڵ�
{
    float timer;
    public int bust_num;//���� ����
    protected float burstInterval = 0.2f; // 3���� ����
    protected float burstCooldown = 1.0f; // 3���� �� ���� �ð�

    int shotsFired = 0; // �� �߻�� Ƚ���� ����

    protected override void setting(ItemData data)//�����丵�ڵ�
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
        bust_num = data.base_bust_num;//��Ƽ�� ���� �ڵ� �� ����

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
    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // �߻� �ֱ�� ���� �ð��� ���� �߻�
        if (shotsFired < bust_num && timer > burstInterval)
        {
            timer = 0f;
            Fire(); // �߻�
            shotsFired++; // �߻� Ƚ�� ����
        }
        else if (shotsFired >= bust_num && timer > burstCooldown)
        {
            // 3���� �� �� ��, �߻� Ƚ�� ����
            shotsFired = 0;
        }
    }

        public override void LevelUp(float damage, int count, float speed, int bust_num)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;
        this.bust_num += bust_num;
    }
}

