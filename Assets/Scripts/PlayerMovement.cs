using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    
    [Header("Input Movement")]
    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode Left;
    public KeyCode Right;
    public float Speed;
    public float MaxSpeed;
    [Header("Projectile")]
    public KeyCode Fire;
    public float ProjectileSpeed;
    [Tooltip("Angle in degrees")]
    public float ProjectileAngle;
    public GameObject ProjectilePrefab;
    public Transform ProjectileTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_rigidbody.velocity.magnitude > MaxSpeed) return;

        if(Input.GetKey(Forward))
        {
            m_rigidbody.AddForce(Vector3.forward * Speed);
        }

        if(Input.GetKey(Back))
        {
            m_rigidbody.AddForce(-Vector3.forward * Speed);
        }

        if(Input.GetKey(Left))
        {
            m_rigidbody.AddForce(-Vector3.right * Speed);
        }

         if(Input.GetKey(Right))
        {
            m_rigidbody.AddForce(Vector3.right * Speed);
        }
    }
}
