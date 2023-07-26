using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Pickup : MonoBehaviour
{
    public float torque;
     Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        rb.AddTorque(transform.up * torque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
