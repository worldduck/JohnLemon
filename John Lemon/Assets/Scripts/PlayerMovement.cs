using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    public float turnSpeed = 20f; // 회전 속도
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

        // 수평축 입력이 있는지 확인
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        // 수직축 입력이 있는지 확인
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        // 수직, 수평 축 bool값 결합하여 캐릭터가 걷는지 판단
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // 애니메이터 IsWaiking파라미터값 설정
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
