using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Movement Part")]
    private Vector3 input;
    private Vector3 IsoInput;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float turnSpeed = 360;
    private bool canJump = false;

    [Header("Ragdoll Part")]
    [SerializeField] private Rigidbody brasDroit;
    [SerializeField] private Rigidbody brasGauche;
    [SerializeField] private Rigidbody centerMass;


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            centerMass.AddForce(new Vector3(0, 10000, 0));
            Debug.Log("JUMP");
        }

        if (Input.GetButton("GrapDroit"))
        {
            brasDroit.AddForce(new Vector3(15, 1, 1));
            Debug.Log("GRAPD");
        }

        if (Input.GetButton("GrapGauche"))
        {
            brasGauche.AddForce(new Vector3(15, 1, 1));
            Debug.Log("GRAPG");
        }

        //centerMass.AddForce(new Vector3(1, 20, 1));

        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
    }

    void Look()
    {
        if (input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

            var skewedInput = matrix.MultiplyPoint3x4(input);
            IsoInput = skewedInput;


            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        //playerObject.MovePosition(transform.position + (transform.forward * input.magnitude) * playerSpeed * Time.deltaTime);

        //playerObject.transform.position += new Vector3(0, 0,  playerSpeed * Time.deltaTime);

        transform.Translate(IsoInput * input.magnitude * playerSpeed * Time.deltaTime, Space.World);
    }
}
