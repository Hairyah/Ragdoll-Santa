using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    [SerializeField] GrapScript grapDroit;
    [SerializeField] GrapScriptGauche grapGauche;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objet")
        {
            grapDroit.Ungrap();
            grapGauche.UngrapGauche();
            Destroy(other.gameObject);
        }
    }
}