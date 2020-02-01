using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProjectile : Projectile
{
    //Private projectile member variables
    private float m_projectileCooldown;
    private bool m_canFire;

    [Header("Player Projectile")]
    public GameObject Projectile;
    public Transform ProjectileSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_projectileCooldown = 2.0f;
        m_canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_projectileCooldown -= Time.deltaTime;


        if(m_canFire) m_projectileCooldown = 2.0f;


        if (m_projectileCooldown < 0.0f) m_canFire = true;

    }

    IEnumerator SpawnProjectile()
    {
        GameObject o = Instantiate(Projectile);

        Vector3 location = o.transform.position;

        location = ProjectileSpawnTransform.position;

        o.transform.position = location;

        Rigidbody projectileRigidbody = o.GetComponent<Rigidbody>();

        projectileRigidbody.AddForce((ProjectileSpawnTransform.forward) * ProjectileSpeed);

        yield return null;
    }

    void OnFireLimb(InputValue value)
    {
        m_canFire = false;
        StartCoroutine(SpawnProjectile());
    }
}
