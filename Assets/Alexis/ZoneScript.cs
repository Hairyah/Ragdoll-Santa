using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    [SerializeField] GrapScript grapDroit;
    [SerializeField] GrapScriptGauche grapGauche;
    [SerializeField] GameObject confetti;
    private AudioManager _audioManager;
    [SerializeField] GamemanagerScript _gamemanagerScript;


    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _gamemanagerScript = FindObjectOfType<GamemanagerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objet")
        {
            GameObject confetti = Instantiate(this.confetti, other.gameObject.transform.position, Quaternion.identity);
            confetti.transform.GetChild(1).GetComponent<FloatingText>().Init("+ " + other.gameObject.GetComponent<ObjectManager>().stringValue);
            _gamemanagerScript.AddArgent(other.gameObject.GetComponent<ObjectManager>().intValue);

            _audioManager.Play("Honk");
            _audioManager.Play("Money");

            grapDroit.Ungrap();
            grapGauche.UngrapGauche();
            Destroy(other.gameObject);
        }
    }
}