using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

      void Awake()
    {
       coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffx = playerPos.x - myPos.x;
        float diffy = playerPos.y - myPos.y;

        Vector3 playerDir = GameManager.Instance.player.inputVec;

        //�������� ���������� �Ǻ��ϴ� ����
        //float dirX = playerDir.x < 0 ? -1 : 1;
        //  float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                
                if (Mathf.Abs(diffx) > 19.5)
                {
                    
                    //     2. �÷��̾� ������ ��� �ľ��ϴ°�?
                    //     ������ 1 Ȥ�� -1 ���� �޾ƾ� �ϴ�, x / x ���·� ũ�⸦ 1 �� ���߰�
                    //     �и� Ȥ�� ���� ���� ���밪�� �־� ��ȣ�� �츲.

                    transform.Translate(Vector3.right * diffx / Mathf.Abs(diffx) * 40);

                }

                if (Mathf.Abs(diffy) > 19.5)
                {
                    
                    transform.Translate(Vector3.up * diffy / Mathf.Abs(diffy) * 40);
                }

                break;

            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3,3), Random.Range(-3,3),0);
                    transform.Translate(ran +dist *2);//�÷��̾� �̵����⿡ ���� ���� ���� ����
                }
                break;

            case "Boss":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);//�÷��̾� �̵����⿡ ���� ���� ���� ����
                }
                break;

            case "RangeEnemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);//�÷��̾� �̵����⿡ ���� ���� ���� ����
                }
                break;

        }
    }
}
