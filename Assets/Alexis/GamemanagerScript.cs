using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamemanagerScript : MonoBehaviour
{
    public GameObject monGrosPrefab;
    public GameObject monGrosClone;
    public GameObject man;
    public GameObject woman;
    public GameObject player;
    public Vector3 playerStartingPos;
    public Vector3 playerStartingRot;

    public Text timeText;
    public Text moneyText;
    public float timeRemaining = 190;
    public bool timerIsRunning = false;
    private float score = 0;

    private void Awake()
    {
        //Instantiate(monGrosPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Start()
    {
        timerIsRunning = true;
        //monGrosClone = Instantiate(monGrosPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        DisplayTime(timeRemaining);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        if (timeRemaining <= 0)
        {
            Debug.Log("T'as plus le temps, vite !");
            ResetMenu();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddArgent(float valeur)
    {
        score += valeur;
        moneyText.text = score+" $";
    }

    public void SoftReset()
    {
        timerIsRunning = false;
        player.transform.position = playerStartingPos;
        player.transform.rotation = Quaternion.Euler(playerStartingRot);
        man.GetComponent<PathManager>().Init();
        woman.GetComponent<PathManager>().Init();
        
        timerIsRunning = true;
    }

    public void HardReset()
    {
        Destroy(monGrosClone);
        score = 0;
        timeRemaining = 190;
        monGrosClone = Instantiate(monGrosPrefab, Vector3.zero, Quaternion.identity);
        man = monGrosClone.transform.GetChild(0).gameObject;
        woman = monGrosClone.transform.GetChild(1).gameObject;
        player = monGrosClone.transform.GetChild(2).gameObject;
        timerIsRunning = true;
    }

    public void ResetMenu()
    {
        timerIsRunning = false;
        Destroy(man);
        Destroy(woman);
        Destroy(player);
        HardReset(); // Faudra pas l'appeler ici mais quand le joueur appuie sur le bouton de l'UI
    }
}
