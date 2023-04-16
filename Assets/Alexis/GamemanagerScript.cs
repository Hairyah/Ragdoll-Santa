using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamemanagerScript : MonoBehaviour
{
    public GameObject monGrosPrefab;
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
            HardReset();
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
        Destroy(man);
        Destroy(woman);
        Instantiate(man);
        Instantiate(woman);
        player.transform.position = playerStartingPos;
        player.transform.rotation = Quaternion.Euler(playerStartingRot);
        timerIsRunning = true;
    }

    public void HardReset()
    {
        timerIsRunning = false;
        Destroy(monGrosPrefab);
        score = 0;
        timeRemaining = 190;
        Instantiate(monGrosPrefab, Vector3.zero, Quaternion.identity);
        timerIsRunning = true;
    }
}
