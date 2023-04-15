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
    public bool canJump = true;
    public float maxRotation = 0;
    private Vector3 startPosition;
    [SerializeField] private GameObject rotatePoint;

    [SerializeField] private GameObject avant;
    [SerializeField] private GameObject arriere;
    [SerializeField] private GameObject gauche;
    [SerializeField] private GameObject droit;

    [Header("Ragdoll Part")]
    [SerializeField] private Rigidbody brasDroit;
    [SerializeField] private Rigidbody brasGauche;
    [SerializeField] private Rigidbody centerMass;
    [SerializeField] private Rigidbody head;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            centerMass.AddForce(new Vector3(0, 7000, 0));
            brasDroit.AddForce(new Vector3(0, 100, 0));
            brasGauche.AddForce(new Vector3(0, 100, 0));
        }

        if (Input.GetButton("GrapDroit"))
        {
            brasDroit.AddRelativeForce(new Vector3(15, 1, 1));
        }

        if (Input.GetButton("GrapGauche"))
        {
            brasGauche.AddRelativeForce(new Vector3(15, 1, 1));
        }

        head.AddForce(new Vector3(1, 20, 1));

        
        if (canJump)
        {
            GatherInput();
            Look();
        }
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
            var euleurRot = rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y - 90, rot.eulerAngles.z);
            rot = Quaternion.Euler(euleurRot);

            /*if (Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.LookAt(avant.transform, Vector3.up);
                Debug.Log("ahahah");
            }

            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.LookAt(arriere.transform, Vector3.up);
            }*/

            var rotateGlobal = transform.eulerAngles;
            var rotateLocal = playerObject.transform.eulerAngles;
            rotateLocal.x = rotateLocal.x * 0;
            rotateLocal.z = rotateLocal.z * 0;
            
            playerObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -90);
            //rotatePoint.transform.position = playerObject.transform.position;

            /*Debug.Log("Rotate : " + rotatePoint.transform.position);
            Debug.Log("Pelvis : " + playerObject.transform.position);*/

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        /*if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(rotatePoint.transform.position, Vector3.up, 100 * Time.deltaTime);
            Debug.Log("Rotate BORDEL");
        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.RotateAround(rotatePoint.transform.position, Vector3.up, -100 * Time.deltaTime);
        }*/
    }

    void Move()
    {
        //playerObject.MovePosition(transform.position + (transform.forward * input.magnitude) * playerSpeed * Time.deltaTime);

        //playerObject.transform.position += new Vector3(0, 0,  playerSpeed * Time.deltaTime);

        transform.Translate(IsoInput * input.magnitude * playerSpeed * Time.deltaTime, Space.World);
    }
}
