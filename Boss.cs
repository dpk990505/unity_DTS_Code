using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        if (collision.CompareTag("Bullet"))
        {
            base.Taking_Damage(collision.GetComponent<Bullet>().damage);
        }
    }
}
//Å×½ºÆ® ¤·¤¤¸»¤¤¾Î