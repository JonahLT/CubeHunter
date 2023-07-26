using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TestProjectile : MonoBehaviour
{
    public float moveSpeed;
    public float lifeSpan;
    float dieAt;

    // Start is called before the first frame update
    void Start()
    {
        dieAt = Time.time + lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if ( dieAt < Time.time)
        {
            Object.Destroy(gameObject); 

        }

        transform.position += transform.forward * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(gameObject); 
    }
}
