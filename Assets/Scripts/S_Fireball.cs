using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Fireball : MonoBehaviour
{
    //public int damage;
    public float speed;
    public AudioSource travelSound;
    public AudioSource hitSound;
    MeshRenderer mr;
    BoxCollider bc;
    public float lifeSpan;
    float dieAt;
    //public S_PlayerCharacter player;


    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        dieAt = Time.time + lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (dieAt < Time.time)
        {
            Object.Destroy(gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(gameObject);
    }
}
