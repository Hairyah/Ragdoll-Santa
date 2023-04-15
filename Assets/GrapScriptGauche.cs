using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapScriptGauche : MonoBehaviour
{
    [SerializeField] bool isGrappingGauche = false;

    void Update()
    {
        if (Input.GetButton("GrapGauche"))
        {
            isGrappingGauche = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UngrapGauche();
        }
    }

    public void UngrapGauche()
    {
        isGrappingGauche = false;
        Destroy(gameObject.GetComponent<FixedJoint>());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objet" && isGrappingGauche)
        {
            isGrappingGauche = false;

            if (gameObject.GetComponent<FixedJoint>() == null)
            {
                var joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = collision.rigidbody;
            }
        }
    }
}
