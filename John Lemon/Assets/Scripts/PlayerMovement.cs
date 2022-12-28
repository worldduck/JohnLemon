using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    public float turnSpeed = 20f; // ȸ�� �ӵ�
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        // ������ �Է��� �ִ��� Ȯ��
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        // ������ �Է��� �ִ��� Ȯ��
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        // ����, ���� �� bool�� �����Ͽ� ĳ���Ͱ� �ȴ��� �Ǵ�
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // �ִϸ����� IsWaiking�Ķ���Ͱ� ����
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward =
            Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }
   
    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
