using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] LayerMask m_floorMask;
    [SerializeField] float m_speed = 6f;

    Vector3 m_movement;
    //Animator anim;
    Rigidbody m_playerRigidbody;

    float m_camRayLength = 100f;
    float m_horz = 0f;
    float m_vert = 0f;
    float m_coldDown = 0.2f;
    float m_coldDownTimer;
    Ray m_Camray;

    RaycastHit m_floorHit;
    Vector3 m_playerToMouse;

     void Awake()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_coldDown += Time.deltaTime;

        m_horz = Input.GetAxis("Horizontal");
        m_vert = Input.GetAxis("Vertical");

        Move();
        Turning();

        if(Input.GetButton("Fire1") && m_coldDownTimer > m_coldDown)
        {
            Punch();
        }


    }

    void Move()
    {
        m_movement.Set(m_vert, 0f, m_horz * -1);

        m_movement = m_movement.normalized * m_speed * Time.deltaTime;

        m_playerRigidbody.MovePosition(transform.position + m_movement);
    }

    void Turning()
    {
        m_Camray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(m_Camray.origin, m_Camray.direction * m_camRayLength, Color.red);

        if (Physics.Raycast(m_Camray, out m_floorHit, m_camRayLength, m_floorMask))
        {
            m_playerToMouse = m_floorHit.point - transform.position;

            m_playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(m_playerToMouse, Vector3.up);

            m_playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Punch()
    {
        m_coldDownTimer = 0f;
        //anim.SetTrigger("Punch");
        Debug.Log("Punch");
    }
}
