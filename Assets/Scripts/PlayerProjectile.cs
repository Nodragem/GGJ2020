using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProjectile : Projectile
{
    //Private projectile member variables
    private float m_projectileCooldown;
    private bool m_canFire;
    private int m_projectileIndex;


    [Header("Player Projectile")]
    public List<GameObject> Projectiles;
    public List<GameObject> Limbs;
    public Transform ProjectileSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_projectileCooldown = 2.0f;
        m_canFire = true;
        m_projectileIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        m_projectileCooldown -= Time.deltaTime;


        if(m_canFire) m_projectileCooldown = 2.0f;


        if (m_projectileCooldown < 0.0f) m_canFire = true;

    }



    IEnumerator SpawnArm()
    {
        GameObject o = Instantiate(Projectiles[0]);

        Arm limb = o.GetComponent<Arm>();

        bool isArm = false;
       
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Arm arm))
            {
                if (!arm.gameObject.activeSelf) continue;
                int index = Limbs.IndexOf(go);
                Limbs[index].SetActive(false);
                isArm = true;
                break;
            }
        }

        if (!isArm)
        {
            Destroy(o);

            yield return null;
        }
        

        Vector3 location = o.transform.position;

        location = ProjectileSpawnTransform.position;

        o.transform.position = location;

        Rigidbody projectileRigidbody = o.GetComponentInChildren<Rigidbody>();

        projectileRigidbody.AddForce((ProjectileSpawnTransform.forward) * ProjectileSpeed);

        yield return null;
    }

    IEnumerator SpawnLeg()
    {


        GameObject o = Instantiate(Projectiles[1]);

        Leg limb = o.GetComponent<Leg>();

        bool isLeg = false;
        
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Leg leg))
            {
                if (!leg.gameObject.activeSelf) break;
                int index = Limbs.IndexOf(go);
                Limbs[index].SetActive(false);
                isLeg = true;
                break;
            }
        }

        if (!isLeg)
        {
            Destroy(o);

            yield return null;
        }


        Vector3 location = o.transform.position;

        location = ProjectileSpawnTransform.position;

        o.transform.position = location;

        Rigidbody projectileRigidbody = o.GetComponentInChildren<Rigidbody>();

        projectileRigidbody.AddForce((ProjectileSpawnTransform.forward) * ProjectileSpeed);

        yield return null;
    }

   

    void OnFireArm()
    {
        m_canFire = false;
        StartCoroutine(SpawnArm());
    }

    void OnFireLeg()
    {
        m_canFire = false;
        StartCoroutine(SpawnLeg());
    }

    void FindArmSetActiveTrue()
    {
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Arm arm2))
            {
                int index = Limbs.IndexOf(go);
                Limbs[index].SetActive(true);
                break;
            }
        }
    }


    void FindLegSetActiveTrue()
    {
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Leg leg))
            {
                int index = Limbs.IndexOf(go);
                Limbs[index].SetActive(true);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody)
        {
            GameObject o = collision.gameObject.GetComponentInParent<Limb>().gameObject;

            Limb limb = o.GetComponent<Limb>();

            if(limb is Arm arm)
            {
                FindArmSetActiveTrue();
                Destroy(o);
            }

            if (limb is Leg leg)
            {
                FindLegSetActiveTrue();
                Destroy(o);
            }
        }
    }
}
