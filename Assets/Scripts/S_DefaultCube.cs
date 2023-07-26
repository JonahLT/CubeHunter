using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class S_DefaultCube : MonoBehaviour
{
    //mouth feel
    public int healthPoints = 10;
    AudioSource hitSound;
    public AudioSource dieSound;
    private Color originalColour;
    public Rigidbody rb;
    public float tombstoneheight;
    bool dropsTombstone;
    public float torque;

    //death
    bool isDead;
    public float deadTime;
    float timeToDie;
    public GameObject tombstone;

    //behaviour and attacking
    public bool isAwake;
    bool hasBeenHit;
    float nextFire;
    public float fireDelay;
    public float moveSpeed;
    public float fireDelayModRange;
    public GameObject fireBall;
    Transform playerPos;
    public Transform bodyPos;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        //get using dif method later???
        isDead = false;
        timeToDie = 0;
        hitSound = GetComponent<AudioSource>();
        originalColour = GetComponentInChildren<Renderer>().material.color;
        rb = GetComponent<Rigidbody>();
        hasBeenHit = false;
        agent = GetComponent<NavMeshAgent>();
        nextFire = -1;
    }

    // Update is called once per frame
    void Update()
    {
        deathCheck();
        if (!isDead && isAwake)
        {
            attack();
            
            agent.SetDestination(playerPos.position);

        }

        hasBeenHit = false;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile" && !hasBeenHit)
        {
            healthPoints--;
            if (!isAwake)
            {
                wakeUp();
                nextFire += 1f;
            }
            
            if(healthPoints > 0)
            {
                StartCoroutine("EnemyFlash");
                hitSound.Play();
                hasBeenHit = true;
            } else
            {
                GetComponentInChildren<Renderer>().material.color = Color.white;
                hasBeenHit = true;
            }
            
        }
    }

    public IEnumerator EnemyFlash()

    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<Renderer>().material.color = originalColour;
        StopCoroutine("EnemyFlash");
    }

    
    void deathCheck()
    {
        if (healthPoints <= 0 && !isDead)
        {
            dieSound.Play();
            rb.useGravity = false;
            timeToDie = Time.time + deadTime;
            isDead = true;
            
        }

        if (isDead && timeToDie <= Time.time)
        {
            if (dropsTombstone)
            {
                Instantiate(tombstone, new Vector3(transform.position.x, transform.position.y + tombstoneheight, transform.position.z), transform.rotation);
            }
            
            Object.Destroy(gameObject);
        }
    }

    
    void attack()
    {
        //add in some kind of charge up with visual and audio que
        if (nextFire < Time.time)
        {
            nextFire = Time.time + fireDelay + Random.Range(0f, fireDelayModRange);
            
            Vector3 direction = playerPos.position - bodyPos.transform.position;
            Quaternion aimRotation = Quaternion.LookRotation(direction);
            //Debug.Log("Direction: " + direction);
            Instantiate(fireBall, bodyPos.transform.position, aimRotation);
            

        }

    }

    //public so trigger can call it
    public void wakeUp()
    {
        if (!isAwake)
        {
            //play wake up noise
            Debug.Log(gameObject + "has woken up!");
            isAwake = true;
            playerPos = GameObject.Find("Player").transform;
            agent.SetDestination(playerPos.position);

            //stupid torque stuff lazy
            rb.AddTorque(transform.up * torque);
            rb.AddTorque(transform.right * torque);
            rb.AddTorque(transform.forward * torque);
        }
        


    }
}
