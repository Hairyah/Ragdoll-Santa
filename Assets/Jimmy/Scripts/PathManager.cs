using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathManager : MonoBehaviour
{
    public bool hasSpotted = false;

    private Animator animator;
    //private Controller controller;

    NavMeshAgent navMeshAgent;
    private GameObject player;

    void Start()
    {
        animator = transform.GetComponent<Animator>();

        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log(transform.gameObject.name + "//" + animator.GetParameter(0).name);
    }

    void Update()
    {
        if (hasSpotted)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
