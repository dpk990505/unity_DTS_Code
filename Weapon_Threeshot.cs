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

    public override void Init(ItemData data)//�����丵�ڵ�
    {
        base.Init(data);
        bust_num = data.base_bust_num;//��Ƽ�� ���� �ڵ� �� ����
    }

    void Update()
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

