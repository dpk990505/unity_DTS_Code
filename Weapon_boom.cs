using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_boom : Weapon
{
    float timer;
   

    // Update is called once per frame
    public override void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        

        timer += Time.deltaTime * Player.fire_rate;
        if (timer > speed)
        {
            if (!player.scanner.nearstTargets)//�����Ǵ� ��ǥ�� ������
                return;
            timer = 0f;
            Vector3 targetPos = player.scanner.nearstTargets.position;//����� ���� ��ġ
            Fire(targetPos, Vector3.down);
        }

        
    }
}
