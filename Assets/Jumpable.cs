using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : MonoBehaviour
{
    [SerializeField] private CharacterScript _characterScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Objet")
        {
            _characterScript.canJump = true;
        }
    }
}
