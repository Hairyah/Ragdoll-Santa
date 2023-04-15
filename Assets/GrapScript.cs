using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapScript : MonoBehaviour
{
    [SerializeField] bool isGrapping = false;

    void Update()
    {

        if (Input.GetButton("GrapDroit"))
        {
            isGrapping = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Ungrap();
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
