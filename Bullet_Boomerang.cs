using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Boomerang : Bullet
{
    void Update()
    {
        if (Life_time >= 0)
        {
            timer += Time.deltaTime;
            rigid.velocity = dir * speed * (1 - 2 * timer / Life_time);
            if (timer >= Life_time)
            {
                gameObject.SetActive(false);//비활성화
                Debug.Log("시간초과 비활성화");
            }

        }
    }
}