using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Vector3 m_movement;
    private bool m_isMoving;
    private bool m_isGameStart;
    private float m_moveTimer;

    [Header("Input Movement")]
    public KeyCode Forward;
    public KeyCode Left;
    public KeyCode Right;
    public float Speed;
    public float MaxSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_isGameStart = false;
        PlayerManager.Instance.OnGameStart += OnGameStart;
        m_moveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isGameStart) return;

        Vector3 preDir = m_rigidbody.velocity.normalized;

        Vector3 dir = new Vector3(m_movement.x, 0, m_movement.y);

        m_rigidbody.velocity =  dir * Speed * Time.deltaTime;

        //if (m_rigidbody.velocity.magnitude < 0.1f)
        //    m_moveTimer = 0.0f;
        //else
        //    m_moveTimer += Time.deltaTime;

        //if (Vector3.Dot(preDir,dir.normalized) < 0.99f ) m_moveTimer = 0.0f;
        if (m_rigidbody.velocity.magnitude > 0.1f)
            m_rigidbody.MoveRotation(Quaternion.LookRotation(m_rigidbody.velocity.normalized));
        //if (m_rigidbody.velocity.magnitude > 0.1f)
        //    m_rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(m_rigidbody.velocity.normalized), m_moveTimer));

        //if(m_moveTimer > 1.0f) m_moveTimer = 0.0f;

        print(m_moveTimer);
    }

    void OnMoveX(InputValue value)
    {
        m_movement.x = value.Get<float>();
    }

    void OnMoveY(InputValue value)
    {
        m_movement.y = value.Get<float>();
    }

    void OnGameStart()
    {
        m_isGameStart = true;
    }
}
