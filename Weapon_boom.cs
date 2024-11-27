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
            if (!player.scanner.nearstTargets)//지정되는 목표가 없을시
                return;
            timer = 0f;
            Vector3 targetPos = player.scanner.nearstTargets.position;//가까운 적의 위치
            Fire(targetPos, Vector3.down);
        }

        
    }
}
