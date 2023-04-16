using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapScript : MonoBehaviour
{
    [SerializeField] bool isGrapping = false;
    [SerializeField] TrailRenderer handTrailRenderer;

    void Update()
    {

        if (Input.GetButton("GrapDroit"))
        {
            isGrapping = true;
        }

        if (Input.GetButton("Ungrap"))
        {
            Ungrap();
        }

        if (isGrapping)
        {
            handTrailRenderer.emitting = true;
        }
        else
        {
            handTrailRenderer.emitting = false;
        }
    }

    public void Ungrap()
    {
        isGrapping = false;
        Destroy(gameObject.GetComponent<FixedJoint>());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objet" && isGrapping)
        {
            isGrapping = false;

            if (gameObject.GetComponent<FixedJoint>() == null)
            {
                var joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = collision.rigidbody;
            }
        }
    }
}
