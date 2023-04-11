using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    private float speedMultiplier = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 velocity = Input.GetAxis("Horizontal")
            * transform.right * speed
            + Input.GetAxis("Vertical")
            * transform.forward * speed;
        velocity.y = 0f;
        velocity *= speedMultiplier;
        transform.GetComponent<Rigidbody>().velocity = velocity;
    }
}
