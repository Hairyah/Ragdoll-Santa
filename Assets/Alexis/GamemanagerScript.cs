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

    public GameObject resetMenu;
    public GameObject hud;
    public GameObject looneyToon;

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
        resetMenu.SetActive(false);
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
        man.GetComponent<PathManager>().Init();
        woman.GetComponent<PathManager>().Init();
        player.GetComponent<CharacterScript>().enabled = false;
        looneyToon.GetComponent<Animator>().SetBool("hasBeenTouched", true);
        StartCoroutine(LooneyToon());
    }

    public void HardReset()
    {
        resetMenu.SetActive(false);
        hud.SetActive(true);
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
        resetMenu.SetActive(true);
        hud.SetActive(false);
    }

    IEnumerator LooneyToon()
    {
        yield return new WaitForSeconds(1);
        looneyToon.GetComponent<Animator>().SetBool("hasBeenTouched", false);
        player.transform.position = playerStartingPos;
        player.transform.rotation = Quaternion.Euler(playerStartingRot);
        yield return new WaitForSeconds(1);
        timerIsRunning = true;
        player.GetComponent<CharacterScript>().enabled = true;
        player.GetComponent<CharacterScript>().hasTouchedNPC = false;
    }
}
