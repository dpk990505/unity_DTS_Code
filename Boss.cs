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

        hp -= collision.GetComponent<Bullet>().damage;

        if (hp > 0)
        {
            anim.SetTrigger("Hit");
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            sprite.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();

            if (GameManager.Instance.isLive)
            { AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead); }
        }
    }
}
