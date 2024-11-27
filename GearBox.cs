using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GearBox : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    // 충돌 감지 (Trigger 이벤트)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 경우
        if (collision.CompareTag("Player"))
        {
            // Animator의 Trigger 매개변수 설정
            animator.SetTrigger("PlayAnimation");
            Show();
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        // 지정한 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}
