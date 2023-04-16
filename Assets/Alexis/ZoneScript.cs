using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    [SerializeField] GrapScript grapDroit;
    [SerializeField] GrapScriptGauche grapGauche;
    [SerializeField] GameObject confetti;
    private AudioManager _audioManager;


    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objet")
        {
            Instantiate(confetti, other.gameObject.transform.position, Quaternion.identity);
            _audioManager.Play("Honk");

            grapDroit.Ungrap();
            grapGauche.UngrapGauche();
            Destroy(other.gameObject);
        }
    }
}