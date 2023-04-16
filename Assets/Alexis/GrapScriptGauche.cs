using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapScriptGauche : MonoBehaviour
{
    [SerializeField] bool isGrappingGauche = false;
    [SerializeField] TrailRenderer handTrailRenderer;

    void Update()
    {
        if (Input.GetButton("GrapGauche"))
        {
            isGrappingGauche = true;
        }

        if (Input.GetButton("Ungrap"))
        {
            UngrapGauche();
        }

        if (isGrappingGauche)
        {
            handTrailRenderer.emitting = true;
        }
        else
        {
            handTrailRenderer.emitting = false;
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
