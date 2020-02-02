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
                //if (arm.ID == LegNArm[1].GetComponent<Arm>().ID)
                //{
                    LegNArm[1].SetActive(true);
                    //ActivationEffect.SetActive(true);
                    UpdateManager();
                    Destroy(limb.gameObject.transform.parent.gameObject);
                //}
                //else
                //{
                //    Rigidbody rb = other.gameObject.GetComponentInChildren<Rigidbody>();

                //    rb.AddRelativeForce(-rb.velocity / 3);
                //}

            }

            if (limb is Leg leg)
            {
                //if (leg.ID == LegNArm[0].GetComponent<Arm>().ID)
                //{
                    LegNArm[0].SetActive(true);
                    //ActivationEffect.SetActive(true);
                    UpdateManager();
                    Destroy(limb.gameObject.transform.parent.gameObject);
                //}
                //else
                //{
                //    Rigidbody rb = other.gameObject.GetComponentInChildren<Rigidbody>();

                //    rb.AddRelativeForce(-rb.velocity / 3);
                //}

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

    
