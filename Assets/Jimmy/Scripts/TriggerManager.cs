using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    PathManager pathManager;

    float walkingSpeed = 3.5f;
    float runningSpeed = 7f;

    void Start()
    {
        pathManager = transform.parent.GetComponent<PathManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerEnter")
        {
            pathManager.hasSpotted = true;
            pathManager.hasBeenChase = true;
            pathManager.navMeshAgent.speed = runningSpeed;
            Debug.Log(transform.parent.name + " Trigger Enter");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerExit")
        {
            pathManager.hasSpotted = false;
            pathManager.navMeshAgent.speed = walkingSpeed;
            Debug.Log(transform.parent.name + " Trigger Exit");
        }
    }
}
