using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    PathManager pathManager;
    private GameObject man;
    [SerializeField] GameObject floatingText;

    float walkingSpeed = 2f;
    float runningSpeed = 4f;

    void Start()
    {
        pathManager = transform.parent.GetComponent<PathManager>();
        man = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerEnter")
        {
            if (!pathManager.hasSpotted)
            {
                /*GameObject newFloatingText = Instantiate(floatingText, man.transform);
                newFloatingText.transform.position = new Vector3(man.transform.position.x, man.transform.position.y + 1, man.transform.position.z);*/
                GameObject newFloatingText = Instantiate(floatingText, new Vector3(man.transform.position.x, man.transform.position.y + 1, man.transform.position.z), Quaternion.identity);
                newFloatingText.transform.SetParent(man.transform);
            }

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
