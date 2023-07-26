using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_PlayerCharacter : MonoBehaviour
{

    //general
    public int currentHealth;
    public int maxHealth;
    bool isDead;
    public float immuneTime;
    float immuneExpire;
    public CPMPlayer CPMPlayer;

    //weapon
    public S_RectPrism rectPrism;
    GameObject gun;

    //second weapon for duel wield finale
    public S_RectPrism secondRectPrism;
    GameObject secondGun;

    //keys 
    public  bool hasYellowKey;
    public bool hasRedKey;
    public bool hasBlueKey;
    public Image blueKeyDisplay;
    public Image yellowKeyDisplay;
    public Image redKeyDisplay;


    //UI
    public TMPro.TextMeshProUGUI healthDisplay; //make this into hearts or a bar or something later
    public GameObject deathDisplay;


    //audio
    AudioSource pickupSound;
    public AudioSource dmgSound;


    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GetComponent<AudioSource>();

        isDead = false;
        if (maxHealth <= 0)
        {
            maxHealth = 1;
        }
        currentHealth = maxHealth;

        //disables the gun so that the player can pick it up later
        gun = GameObject.Find("Gun_RectPrism");
        gun.SetActive(false);

       //same but with second gun
        secondGun = GameObject.Find("Gun_RectPrism2");
        secondGun.SetActive(false);

        hasBlueKey = false;
        hasYellowKey = false;
        hasRedKey = false;

        blueKeyDisplay.enabled = false;
        yellowKeyDisplay.enabled = false;
        redKeyDisplay.enabled = false;

        deathDisplay.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        healthDisplay.text = ("Health: " + currentHealth + " / " + maxHealth);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fireball") && immuneExpire < Time.time)
        {
            takeDamage();

        } 
        

    }

    private void OnTriggerEnter(Collider other)
    {

        //Keys
        if(other.tag == "RedKey")
        {
            //play sfx
            redKeyDisplay.enabled = true;
            hasRedKey = true;
            Object.Destroy(other.gameObject);
        }

        if (other.tag == "YellowKey")
        {
            //play sfx
            yellowKeyDisplay.enabled = true;
            hasYellowKey = true;
            Object.Destroy(other.gameObject);
        }

        if (other.tag == "BlueKey")
        {
            //play sfx
            blueKeyDisplay.enabled = true;
            hasBlueKey = true;
            Object.Destroy(other.gameObject);
        }

        //Other pickups
        if (other.gameObject.tag == "AmmoPickup")
        {
            pickupSound.Play();
            Debug.Log("Ammo picked up!");
            rectPrism.bullets += 10;
            Object.Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "GunPickup")
        {
            gun.SetActive(true);
            
            Object.Destroy(other.gameObject);
        }
        else if ((other.gameObject.tag == "JumpBootsPickup"))
        {
            //play sfx
            CPMPlayer.hasJumpBoots = true;
            Object.Destroy(other.gameObject);
        } else if ((other.gameObject.name == "DuelWeildPickup"))
        {
            secondGun.SetActive(true);
            Object.Destroy(other.gameObject);
        }
    }

    public void takeDamage()
    {
        if(immuneExpire < Time.time && !isDead)
        {
            currentHealth--;
            dmgSound.Play();
            immuneExpire = Time.time + immuneTime;
            if (currentHealth <= 0)
            {
                Debug.Log("You are Dead!");
                isDead = true;
                
                
                CPMPlayer.enabled = false;

                deathDisplay.SetActive(true);

            }
        }
  
    }
}
