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
                GameObject newFloatingText = Instantiate(floatingText, new Vector3(man.transform.position.x, man.transform.position.y + 2, man.transform.position.z), Quaternion.identity);
                newFloatingText.transform.SetParent(man.transform);
                newFloatingText.GetComponent<FloatingText>().Init("jhf");
            }

            pathManager.hasSpotted = true;
            pathManager.hasBeenChase = true;
            pathManager.navMeshAgent.speed = runningSpeed;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.gameObject.tag == "TriggerExit")
        {
            pathManager.hasSpotted = false;
            pathManager.navMeshAgent.speed = walkingSpeed;
        }
    }
}
