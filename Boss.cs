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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("RangeEnemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
//Å×½ºÆ® ¤·¤¤¸»¤¤¾Î