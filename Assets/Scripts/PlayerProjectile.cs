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
    public Material[] RobotMaterials;
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
                Material[] mats = o.GetComponentInChildren<SkinnedMeshRenderer>().materials;
                Material[] mats2 = go.GetComponentInChildren<SkinnedMeshRenderer>().materials;

                mats[0] = mats2[0];
                mats[1] = mats2[1];

                o.GetComponentInChildren<SkinnedMeshRenderer>().materials = mats;

                go.SetActive(false);
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
                
                Material[] mats = o.GetComponentInChildren<SkinnedMeshRenderer>().materials;
                Material[] mats2 = go.GetComponentInChildren<SkinnedMeshRenderer>().materials;

                mats[0] = mats2[0];
                mats[1] = mats2[1];

                o.GetComponentInChildren<SkinnedMeshRenderer>().materials = mats;

                go.SetActive(false);
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

    bool FindArmSetActiveTrue(GameObject arm)
    {
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Arm arm2))
            {
                if (go.activeSelf) continue;
                go.SetActive(true);

                for(int i = 0; i < go.GetComponent<SkinnedMeshRenderer>().materials.Length; ++i)
                {
                    Material robot = go.GetComponent<SkinnedMeshRenderer>().materials[i];
                    Material looseArm = arm.GetComponent<SkinnedMeshRenderer>().materials[i];

                    if (robot == looseArm) continue;

                    robot = looseArm;

                    Material[] mats = go.GetComponent<SkinnedMeshRenderer>().materials;

                    mats[i] = robot;

                    go.GetComponent<SkinnedMeshRenderer>().materials = mats;
                }
                

                return false;
            }
        }
        return true;
    }


    bool FindLegSetActiveTrue(GameObject leg)
    {
        foreach (GameObject go in Limbs)
        {
            if (go.TryGetComponent(out Leg leg2))
            {
                if (go.activeSelf) continue;
                go.SetActive(true);
                
                Material[] mats = go.GetComponent<SkinnedMeshRenderer>().materials;

                mats[1] = RobotMaterials[1];

                go.GetComponent<SkinnedMeshRenderer>().materials = mats;

                Material tmp = RobotMaterials[0];
                RobotMaterials[0] = RobotMaterials[1];
                RobotMaterials[1] = tmp;

                return false;
            }
        }
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<LimbSounds>())
        {
            GameObject o = collision.gameObject.GetComponent<LimbSounds>().gameObject;

            Limb limb = o.GetComponentInParent<Limb>();

            bool remain = true;

            if(limb is Arm arm)
            {
                remain = FindArmSetActiveTrue(limb.gameObject);
            }

            if (limb is Leg leg)
            {
                remain = FindLegSetActiveTrue(limb.gameObject);
            }

            if(!remain)
                Destroy(o.transform.parent.gameObject);
        }
    }
}
