using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathManager : MonoBehaviour
{
    public bool hasSpotted = false;

    private Animator animator;
    public string isWalking = "isWalking";
    public string isRunning = "isRunning";
    public string isSitting = "isSitting";
    public string isStandingSit = "isStandingSit";
    public string isFallingFront = "isFallingFront";
    public string isFallingBack = "isFallingBack";
    public string isStandingBack = "isStandingBack";
    public string isStandingFront = "isStandingFront";
    public string isLaying = "isLaying";
    public string isOpening = "isOpening";
    public string isClosing = "isClosing";
    public string isDrinking = "isDrinking";
    public string isPraying = "isPraying";
    public string isPlaying = "isPlaying";
    public string isWoman = "isWoman";

    public int index = 0;
    [SerializeField] Transform[] destinations;
    private Vector3[] destinationsPos;

    NavMeshAgent navMeshAgent;
    private GameObject player;

    void Awake()
    {
        animator = transform.GetComponent<Animator>();

        destinationsPos = new Vector3[destinations.Length];

        for (int i = 0; i < destinations.Length; ++i)
        {
            //Debug.Log(destinations[i]);
            destinationsPos[i] = new Vector3(destinations[i].position.x, destinations[i].position.y, destinations[i].position.z);
        }

        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        SetNewDestination(0);
    }

    void Update()
    {
        if (hasSpotted)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            if (transform.position == navMeshAgent.destination)
            {
                switch (transform.name)
                {
                    case "Man":

                        switch (index)
                        {
                            case 0:
                                StartCoroutine(WaitingPause(0));
                                break;

                            case 1:
                                StartCoroutine(WaitingPause(0));
                                break;

                            case 2:
                                StartCoroutine(WaitingPause(0));
                                break;
                        }

                        break;

                    case "Woman":

                        switch (index)
                        {
                            case 0:
                                StartCoroutine(WaitingPause(0));
                                break;

                            case 1:
                                StartCoroutine(WaitingPause(0));
                                break;

                            case 2:
                                StartCoroutine(WaitingPause(0));
                                break;
                        }

                        break;
                }

                if (index == destinations.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    ++index;
                }
                
                SetNewDestination(index);
            }
        }
    }

    public void SetNewDestination(int index)
    {
        navMeshAgent.SetDestination(destinationsPos[index]);
    }

    public int GetIndexOfTransformInArray(Transform transform, Transform[] array)
    {
        int index = -1;

        if (array.Length > 0)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (transform == array[i])
                {
                    index = i;
                    break;
                }
            }
        }

        return index;
    }

    public int GetIndexOfVectorInArray(Vector3 vector, Vector3[] array)
    {
        int index = -1;

        if (array.Length > 0)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (vector == array[i])
                {
                    index = i;
                    break;
                }
            }
        }

        return index;
    }

    IEnumerator WaitingPause(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
