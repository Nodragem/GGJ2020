using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbTester : MonoBehaviour
{
    public GameObject[] prefabs;
    public float force;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, transform.rotation);
            obj.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * force);
        }
    }
}
