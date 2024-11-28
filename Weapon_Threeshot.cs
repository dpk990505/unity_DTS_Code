using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Threeshot : Weapon_front
{
    float timer;
    int shotsFired = 0; // �� �߻�� Ƚ���� ����
    public float burstInterval = 0.2f; // 3���� ����
    public float burstCooldown = 1.0f; // 3���� �� ���� �ð�

    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;

        // �߻� �ֱ�� ���� �ð��� ���� �߻�
        if (shotsFired < 3 && timer > burstInterval)
        {
            timer = 0f;
            Fire(); // �߻�
            shotsFired++; // �߻� Ƚ�� ����
        }
        else if (shotsFired >= 3 && timer > burstCooldown)
        {
            // 3���� �� �� ��, �߻� Ƚ�� ����
            shotsFired = 0;
        }
    }
}
