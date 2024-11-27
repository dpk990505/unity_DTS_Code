using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GearBox : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    // �浹 ���� (Trigger �̺�Ʈ)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ���� ���
        if (collision.CompareTag("Player"))
        {
            // Animator�� Trigger �Ű����� ����
            animator.SetTrigger("PlayAnimation");
            Show();
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
