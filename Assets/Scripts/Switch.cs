using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    bool m_isDone;

    public GameObject[] LegNArm;
    public GameObject ActivationEffect;

    // Use this for initialization
    void Start()
    {
        m_isDone = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Limb limb = other.gameObject.GetComponentInParent<Limb>();

        if (limb)
        {
            if (m_isDone) return;
            

            if (limb is Arm arm)
            {

                Material[] ThrownArmMats = arm.GetComponent<SkinnedMeshRenderer>().materials;
                Material[] ArmInCircuitMats = LegNArm[1].GetComponentInChildren<SkinnedMeshRenderer>().materials;

                print(ThrownArmMats);
                print(ArmInCircuitMats);
                print(ThrownArmMats[1] == ArmInCircuitMats[1]);
                

                if (ThrownArmMats[1].color == ArmInCircuitMats[1].color)
                {
                    LegNArm[1].SetActive(true);
                    //ActivationEffect.SetActive(true);
                    UpdateManager();
                    Destroy(limb.gameObject.transform.parent.gameObject);
                }
                else
                {
                   Rigidbody rb = other.gameObject.GetComponentInChildren<Rigidbody>();

                    rb.AddRelativeForce(-rb.velocity / 3);
                }

            }

            if (limb is Leg leg)
            {
                Material[] ThrownLegMats = leg.GetComponent<SkinnedMeshRenderer>().materials;
                Material[] LegInCircuitMats = LegNArm[0].GetComponentInChildren<SkinnedMeshRenderer>().materials;

                print(ThrownLegMats);
                print(LegInCircuitMats);
                print(ThrownLegMats[1] == LegInCircuitMats[1]);

                if (ThrownLegMats[1].color == LegInCircuitMats[1].color)
                {
                    LegNArm[0].SetActive(true);
                    //ActivationEffect.SetActive(true);
                    UpdateManager();
                    Destroy(limb.gameObject.transform.parent.gameObject);
                }
                else
                {
                    Rigidbody rb = other.gameObject.GetComponentInChildren<Rigidbody>();

                    rb.AddRelativeForce(-rb.velocity / 3);
                }

            }



        }
        else if (other.gameObject.GetComponentInChildren<Rigidbody>())
        {
            Rigidbody rb = other.gameObject.GetComponentInChildren<Rigidbody>();

            rb.AddRelativeForce(-rb.velocity / 3);
        }


    }

    void UpdateManager()
    {
        m_isDone = true;
        SwitchManager.Instance.UpdateSwitchCounter();
    }
}

    
