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
            Debug.Log("Trigger Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerExit")
        {
            pathManager.hasSpotted = false;
            Debug.Log("Trigger Exit");
        }
    }
}
