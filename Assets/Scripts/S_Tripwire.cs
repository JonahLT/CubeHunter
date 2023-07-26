using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Tripwire : MonoBehaviour
{
    public S_Wall[] walls = new S_Wall[1];
    public S_DefaultCube[] cubes = new S_DefaultCube[1];
    bool wasTriggered;

    private void Start()
    {
        wasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !wasTriggered)
        {
            Debug.Log("Tripwire " + gameObject.name + " was triggered!");

            for (int i = 0; i < cubes.Length; i++)
            {
                cubes[i].wakeUp();
                
            }

            for (int i = 0;i < walls.Length; i++)
            {
                walls[i].Awake();
                
            }

            Object.Destroy(gameObject);
        }
    }
}
