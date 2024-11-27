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

        //왼쪽인지 오른쪽인지 판별하는 로직
        //float dirX = playerDir.x < 0 ? -1 : 1;
        //  float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                
                if (Mathf.Abs(diffx) > 19.5)
                {
                    
                    //     2. 플레이어 방향은 어떻게 파악하는가?
                    //     방향은 1 혹은 -1 값을 받아야 하니, x / x 형태로 크기를 1 로 맞추고
                    //     분모 혹은 분자 에만 절대값을 넣어 부호를 살림.

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
                    transform.Translate(ran +dist *2);//플레이어 이동방향에 따라 맞은 편에서 등장
                }
                break;

            case "Boss":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);//플레이어 이동방향에 따라 맞은 편에서 등장
                }
                break;

            case "RangeEnemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);//플레이어 이동방향에 따라 맞은 편에서 등장
                }
                break;

        }
    }
}
