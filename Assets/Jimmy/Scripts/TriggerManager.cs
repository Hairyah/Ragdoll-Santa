using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    PathManager pathManager;

    void Start()
    {
        pathManager = transform.parent.GetComponent<PathManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerEnter")
        {
            pathManager.hasSpotted = true;
            Debug.Log(transform.parent.name + " Trigger Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerExit")
        {
            pathManager.hasSpotted = false;
            Debug.Log(transform.parent.name + " Trigger Exit");
        }
    }
}
