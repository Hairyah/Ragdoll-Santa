using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathManager : MonoBehaviour
{
    [SerializeField] GamemanagerScript _gamemanagerScript;

    [SerializeField] public Vector3 startingPos;
    [SerializeField] public Vector3 startingRot;
    public bool hasSpotted = false;
    public bool hasBeenChase = false;

    public GameObject drum;
    public GameObject arcade;

    public Animator animator;
    public string isWalking = "isWalking";
    public string isRunning = "isRunning";
    public string isSitting = "isSitting";
    public string isSitPlaying = "isSitPlaying";
    public string isSitPointing = "isSitPointing";
    public string isSitYelling = "isSitYelling";
    public string isSitClaping = "isSitClaping";
    public string isSitLaughing = "isSitLaughing";
    public string isOpening = "isOpening";
    public string isDrinking = "isDrinking";
    public string isCooking = "isCooking";
    public string isStandPointing = "isStandPointing";
    public string isStandLooking = "isStandLooking";
    public string isPlaying = "isPlaying";

    public string isWoman = "isWoman";

    public int indexFinal = 0;
    public int index = 0;
    public bool hasReachedPoint = false;
    [SerializeField] Transform[] destinations;
    private Vector3[] destinationsFinalPos;
    private Vector3[,] destinationsPointPos;
    public bool hasDesinationChanged = false;
    private string hasDesinationChangedName = "hasDesinationChanged";
    public GameObject rig;
    private Vector3 euler;

    public NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;

    void Awake()
    {
        //animator = transform.GetComponent<Animator>();

        destinationsFinalPos = new Vector3[destinations.Length];
        destinationsPointPos = new Vector3[destinations.Length, 1];

        for (int i = 0; i < destinations.Length; ++i)
        {
            destinationsFinalPos[i] = new Vector3(destinations[i].position.x, destinations[i].position.y, destinations[i].position.z);
            destinationsPointPos[i, 0] = new Vector3(destinations[i].GetChild(0).position.x, destinations[i].GetChild(0).position.y, destinations[i].GetChild(0).position.z);
        }

        navMeshAgent = transform.GetComponent<NavMeshAgent>();

        Init();
    }

    void Update()
    {
        if (transform.name == "Man" && animator.GetBool(isPlaying) == true)
        {
            arcade.GetComponent<AudioSource>().enabled = true;
        }
        else if (transform.name == "Man" && animator.GetBool(isSitPlaying) == true)
        {
            drum.GetComponent<AudioSource>().enabled = true;
        }
        else if (transform.name == "Man" && animator.GetBool(isPlaying) == false)
        {
            arcade.GetComponent<AudioSource>().enabled = false;
        }
        else if (transform.name == "Man" && animator.GetBool(isSitPlaying) == false)
        {
            drum.GetComponent<AudioSource>().enabled = false;
        }

        if (hasSpotted)
        {
            navMeshAgent.SetDestination(player.transform.position);
            ResetAllBool();
        }
        else
        {
            if (hasReachedPoint)
            {
                SetNewDestination(index);
                if (transform.position == navMeshAgent.destination)
                {
                    if (!hasDesinationChanged)
                    {
                        hasBeenChase = false;
                        ResetAllBool();
                        hasReachedPoint = true;
                        SetAnimation();
                    }
                }
            }
            else
            {
                SetNewDestinationPoint(index);
                if (transform.position == navMeshAgent.destination)
                {
                    hasBeenChase = false;
                    hasReachedPoint = true;
                    SetNewDestination(index);
                }
            }

            
        }
    }

    public void Init()
    {
        transform.position = startingPos;
        transform.rotation = Quaternion.Euler(startingRot);

        switch (transform.name)
        {
            case "Man":
                animator.SetBool(isWoman, false);
                break;

            case "Woman":
                animator.SetBool(isWoman, true);
                break;
        }

        hasSpotted = false;
        hasBeenChase = false;
        hasReachedPoint = false;
        hasDesinationChanged = false;
        animator.SetBool(isRunning, false);
}

    public void SetAnimation()
    {
        hasDesinationChanged = true;
        
        switch (transform.name)
        {
            case "Man":

                switch (index)
                {
                    case 0:
                        //Batterie ===> 20.5f
                        StartCoroutine(WaitingToSetNewDestination(20.5f));
                        animator.SetBool(isSitting, true);
                        animator.SetBool(isSitPlaying, true);
                        StartCoroutine(SetBoolWithDelay(isSitting, false, 15f));
                        StartCoroutine(SetBoolWithDelay(isSitPlaying, false, 15f));
                        break;

                    case 1:
                        //Sofa ===> 20f
                        StartCoroutine(WaitingToSetNewDestination(20f));
                        animator.SetBool(isSitting, true);
                        animator.SetBool(isSitPointing, true);
                        StartCoroutine(SetBoolWithDelay(isSitPointing, false, 5f));
                        StartCoroutine(SetBoolWithDelay(isSitYelling, true, 8f));
                        StartCoroutine(SetBoolWithDelay(isSitYelling, false, 10f));
                        StartCoroutine(SetBoolWithDelay(isSitPointing, true, 14f));
                        StartCoroutine(SetBoolWithDelay(isSitPointing, false, 16f));
                        StartCoroutine(SetBoolWithDelay(isSitting, true, 18f));
                        break;

                    case 2:
                        //Plaque de cuisson ===> 40f
                        StartCoroutine(WaitingToSetNewDestination(40f));
                        animator.SetBool(isCooking, true);
                        StartCoroutine(SetBoolWithDelay(isCooking, false, 30f));
                        break;

                    case 3:
                        //Arcade ===> 19.5f
                        StartCoroutine(WaitingToSetNewDestination(19.5f));
                        animator.SetBool(isPlaying, true);
                        StartCoroutine(SetBoolWithDelay(isPlaying, false, 18f));
                        break;
                }

                break;

            case "Woman":

                switch (index)
                {
                    case 0:
                        //Frigo ===> 20f
                        StartCoroutine(WaitingToSetNewDestination(20f));
                        animator.SetBool(isOpening, true);
                        break;

                    case 1:
                        //Fauteuil ===> 20f
                        StartCoroutine(WaitingToSetNewDestination(20f));
                        animator.SetBool(isSitting, true);
                        StartCoroutine(SetBoolWithDelay(isSitLaughing, true, 5f));
                        StartCoroutine(SetBoolWithDelay(isSitLaughing, false, 15f));
                        StartCoroutine(SetBoolWithDelay(isSitting, false, 16f));
                        break;

                    case 2:
                        //Tableau 1 ===> 18f
                        StartCoroutine(WaitingToSetNewDestination(18f));
                        animator.SetBool(isStandPointing, true);
                        StartCoroutine(SetBoolWithDelay(isStandPointing, false, 1f));
                        StartCoroutine(SetBoolWithDelay(isStandLooking, true, 5f));
                        StartCoroutine(SetBoolWithDelay(isStandLooking, false, 7f));
                        StartCoroutine(SetBoolWithDelay(isStandPointing, true, 12f));
                        StartCoroutine(SetBoolWithDelay(isStandPointing, false, 13.5f));
                        break;

                    case 3:
                        //Tableau 2 ===> 18f
                        StartCoroutine(WaitingToSetNewDestination(18f));
                        animator.SetBool(isStandPointing, true);
                        StartCoroutine(SetBoolWithDelay(isStandPointing, false, 1f));
                        StartCoroutine(SetBoolWithDelay(isStandLooking, true, 5f));
                        StartCoroutine(SetBoolWithDelay(isStandLooking, false, 7f));
                        StartCoroutine(SetBoolWithDelay(isStandPointing, true, 12f));
                        StartCoroutine(SetBoolWithDelay(isStandPointing, false, 13.5f));
                        break;

                    case 4:
                        //Toilettes ===> 15.5f
                        StartCoroutine(WaitingToSetNewDestination(15.5f));
                        animator.SetBool(isSitting, true);
                        StartCoroutine(SetBoolWithDelay(isSitting, false, 13f));
                        break;
                }

                break;
        }
    }

    public void SetNewDestination(int index)
    {
        float distance = Vector3.Distance(transform.position, navMeshAgent.destination);

        if (hasSpotted && animator.GetBool(isRunning) == false)
        {
            animator.SetBool(isWalking, false);
            animator.SetBool(isRunning, true);
        }

        if (!hasSpotted && animator.GetBool(isWalking) == false && distance > 1)
        {
            animator.SetBool(isRunning, false);
            animator.SetBool(isWalking, true);
        }
        else if (!hasSpotted && distance < 1)
        {
            animator.SetBool(isRunning, false);
            animator.SetBool(isWalking, false);
        }



        navMeshAgent.SetDestination(destinationsFinalPos[index]);
    }

    public void SetNewDestinationPoint(int index)
    {
        float distance = Vector3.Distance(transform.position, navMeshAgent.destination);

        if (hasSpotted && animator.GetBool(isRunning) == false)
        {
            animator.SetBool(isWalking, false);
            animator.SetBool(isRunning, true);
        }

        if (!hasSpotted && animator.GetBool(isWalking) == false)
        {
            animator.SetBool(isRunning, false);
            animator.SetBool(isWalking, true);
        }

        navMeshAgent.SetDestination(destinationsPointPos[index, 0]);
    }

    IEnumerator WaitingToSetNewDestination(float time)
    {
        bool monGrosBool = false;

        for (int i = 0; i < time; ++i)
        {
            if (hasBeenChase)
            {
                monGrosBool = true;
            }
            yield return new WaitForSecondsRealtime(1);
        }

        if (!int.TryParse(string.Format("{0}", time), out int ignore))
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }
        

        if (!monGrosBool)
        {
            if (index == destinations.Length - 1)
            {
                index = 0;
            }
            else
            {
                ++index;
            }

            SetNewDestinationPoint(index);
            StartCoroutine(SetBoolWithDelay(hasDesinationChangedName, false, 1f));
            hasReachedPoint = false;
        }

    }

    IEnumerator SetBoolWithDelay(string name, float time)
    {
        bool monGrosBool = false;

        for (int i = 0; i < time; ++i)
        {
            if (hasBeenChase)
            {
                monGrosBool = true;
            }
            yield return new WaitForSecondsRealtime(1);
        }

        if (!int.TryParse(string.Format("{0}", time), out int ignore))
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }

        if (!monGrosBool)
        {
            if (name == "hasDesinationChanged")
            {
                if (hasDesinationChanged)
                {
                    hasDesinationChanged = false;
                }
                else if (!hasDesinationChanged)
                {
                    hasDesinationChanged = true;
                }
            }
            else if (animator.GetBool(name))
            {
                animator.SetBool(name, false);
            }
            else if (!animator.GetBool(name))
            {
                animator.SetBool(name, true);
            }
        }

    }

    IEnumerator SetBoolWithDelay(string name, bool status, float time)
    {
        bool monGrosBool = false;

        for (int i = 0; i < time; ++i)
        {
            if (hasBeenChase)
            {
                monGrosBool = true;
            }
            yield return new WaitForSecondsRealtime(1);
        }

        if (!int.TryParse(string.Format("{0}", time), out int ignore))
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }

        if (!monGrosBool)
        {
            if (name == "hasDesinationChanged")
            {
                hasDesinationChanged = status;
            }
            else
            {
                animator.SetBool(name, status);
            }
        }
    }

    public void SetBool(string name)
    {
        if (animator.GetBool(name))
        {
            animator.SetBool(name, false);
        }
        else if (!animator.GetBool(name))
        {
            animator.SetBool(name, true);
        }
    }

    public void GetRotation()
    {
        euler = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 180, transform.rotation.eulerAngles.z);
    }

    public void ApplyRotation()
    {
        if (euler != null)
        {
            transform.rotation = Quaternion.Euler(euler);
        }
    }

    public void ResetAllBool()
    {
        animator.SetBool(isSitting, false);
        animator.SetBool(isSitPlaying, false);
        animator.SetBool(isSitPointing, false);
        animator.SetBool(isSitYelling, false);
        animator.SetBool(isSitClaping, false);
        animator.SetBool(isOpening, false);
        animator.SetBool(isDrinking, false);
        animator.SetBool(isCooking, false);
        animator.SetBool(isStandPointing, false);
        animator.SetBool(isStandLooking, false);
        animator.SetBool(isPlaying, false);

        hasDesinationChanged = false;
        hasReachedPoint = false;
        animator.SetBool(isRunning, true);
    }
}
