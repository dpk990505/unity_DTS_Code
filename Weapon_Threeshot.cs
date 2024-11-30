using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon_front
{
    float timer;

    protected int burst_number = 3;
    protected float burstInterval = 0.2f; // 3���� ����
    protected float burstCooldown = 1.0f; // 3���� �� ���� �ð�

    int shotsFired = 0; // �� �߻�� Ƚ���� ����
    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // �߻� �ֱ�� ���� �ð��� ���� �߻�
        if (shotsFired < burst_number && timer > burstInterval)
        {
            timer = 0f;
            Fire(); // �߻�
            shotsFired++; // �߻� Ƚ�� ����
        }
        else if (shotsFired >= burst_number && timer > burstCooldown)
        {
            // 3���� �� �� ��, �߻� Ƚ�� ����
            shotsFired = 0;
        }
    }
}
