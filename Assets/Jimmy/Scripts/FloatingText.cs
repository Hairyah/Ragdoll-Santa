using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        Init("monGrosBool");
    }

    public void Init(string text)
    {
        switch (transform.parent.tag)
        {
            case "Player":
                Destroy(transform.gameObject, 2f);
                transform.GetComponent<TextMeshPro>().text = text;
                break;

            case "NPC":
                Destroy(transform.gameObject, 2f);
                transform.GetComponent<TextMeshPro>().text = "!";
                _audioManager.Play("Spotted");
                break;

            case "Particles":
                Destroy(transform.parent.gameObject, 2f);
                transform.GetComponent<TextMeshPro>().text = text;
                break;
        }
    }
}
