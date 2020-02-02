using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public string colorID = "blue";
    public GameObject activationEffect;
    // Start is called before the first frame update
    void Start()
    {
        activationEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("touch!");
        Limb limb = other.GetComponentInParent<Limb>();
        
        if(limb==null)
            Debug.Log("touched by robot!");
        else
            Debug.Log("touched by limb");
    }
}
