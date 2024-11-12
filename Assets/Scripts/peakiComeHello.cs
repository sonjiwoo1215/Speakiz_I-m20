using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ�� �����ɴϴ�.
    }

    // �̵� �ִϸ��̼��� ������ ȣ���� �޼���
    public void OnMovementEnd()
    {
        animator.SetTrigger("hello"); // �λ� �ִϸ��̼����� ��ȯ�ϴ� Ʈ���� ����
    }
}
