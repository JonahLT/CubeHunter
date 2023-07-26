using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class S_RectPrism : MonoBehaviour
{
    public float fireDelay;
    public float nextFire;
    public GameObject projectile;
    public Transform projectileSpawn;

    //audio
    AudioSource fireSound;
    RaycastHit hit;
    Camera cam;
    Vector3 projectileTarget;

    //ammo
    public int bullets;
    public int maxBullets;
    public int pickupAmount; // this is stupid and should be on the pickup itself but who cares lol fix later

    //UI
    public TMPro.TextMeshProUGUI bulletDisplay;

    
    


    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        cam = Camera.main;

        if (bullets == 0)
        {
            bullets = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fire();
        bulletDisplay.text = ("Ammo: " + bullets);
        

    }

    void fire()
    {
        if (Input.GetMouseButton(0) && nextFire < Time.time && bullets > 0)
        {
            nextFire = Time.time + fireDelay;
            bullets--;
            //raycast stuff here
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.UseGlobal))
            {
                projectileTarget = hit.point;
                Vector3 direction = projectileTarget - projectileSpawn.transform.position;
                Quaternion aimRotation = Quaternion.LookRotation(direction);
                //Debug.Log("Direction: " + direction);
                Instantiate(projectile, projectileSpawn.transform.position, aimRotation);
               // Debug.Log("Raycast hit something!");
                //Debug.DrawRay(projectileSpawn.position, hit.transform.position - projectileSpawn.position, Color.green, 5f);

            }
            else
            {
                Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                //Debug.Log("Raycast did not hit something");
            }

            

            fireSound.Play();
        }
    }

    //use onEnable to do stuff when the gun is picked up!

   
}
