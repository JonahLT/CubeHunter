using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Wall : MonoBehaviour
{

    public float moveSpeed;
    public GameObject moveTo;
    public bool isOpened;
    bool done;
    public string color;
    public S_PlayerCharacter S_PlayerCharacter;
    // Start is called before the first frame update
    void Start()
    {
   
        isOpened = false;
        done = false;
        moveTo.transform.parent = null;
        if (color != "red" || color != "blue" || color != "yellow")
        {
            color = "red";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened)
        {
         
            transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == moveTo.transform.position)
            {
                //stop sfx
                done = true;
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !done && !isOpened)
        {
            if ((S_PlayerCharacter.hasBlueKey && color == "blue") || (S_PlayerCharacter.hasRedKey && color == "red") || (S_PlayerCharacter.hasYellowKey && color == "yellow")) {
                Awake();
            }
            
        }
    }

    public void Awake()
    {
        isOpened = true;
        Debug.Log("TRIGGERED DOOR");
        isOpened = true;
        //start sfx
    }

}
