using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
    {
    [SerializeField] private Rigidbody _rb;
    private Vector3 _input;
    [SerializeField] private GameObject spawnPlayerPoint;
    private Vector3 spawnPoint;
    private PlayerAttack playerAttack;
    Animator m_anim;


    [SerializeField] LayerMask m_floorMask;
    [SerializeField] float m_speed = 6f;

    Vector3 m_movement;
    Rigidbody m_playerRigidbody;

    float m_camRayLength = 100f;
    float m_horz = 0f;
    float m_vert = 0f;
    float m_coldDown = 0.2f;
    float m_coldDownTimer;
    Ray m_Camray;

    RaycastHit m_floorHit;
    Vector3 m_playerToMouse;

    public Vector3 SpawnPoint
    {
        get { return spawnPoint; }
        set { spawnPoint = value; }
    }

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_playerRigidbody = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();
        spawnPoint = spawnPlayerPoint.transform.position; // Paso el spawn point para que luego pueda actualizarlo dependiendo de cuanto avanzo el jugador en el maze, como checkpoint 
        transform.position = spawnPoint;
    }
    private void Start()
        {
       

        }
    private void Update()
    {        
           GatherInput();
    }
    void FixedUpdate()
    {
        Move();
        Turning();

    }
    void GatherInput()
    {
        m_coldDown += Time.deltaTime;

        m_horz = Input.GetAxis("Horizontal");
        m_vert = Input.GetAxis("Vertical");


        if (Input.GetMouseButtonDown(0))
        {
            playerAttack.Attack();
        }
        
    }
    void Move()
    {
        m_movement.Set(m_vert, 0f, m_horz * -1);

        m_movement = m_movement.normalized * m_speed * Time.deltaTime;

        m_playerRigidbody.MovePosition(transform.position + m_movement);

        AnimateWalk(m_horz, m_vert);
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

    void AnimateWalk(float h, float v)
    {
        bool walking = h != 0f || v != 0f; // Si el jugador se mueve, entonces la animacion de caminar se activa
        m_anim.SetBool("IsWalking", walking); // Cambia el valor de la variable booleana en el animator

    }
}

    
        


