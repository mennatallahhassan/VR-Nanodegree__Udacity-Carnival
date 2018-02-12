using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarnivalScores : MonoBehaviour
{

    public GameObject camera;
    public Text timeRemaninginText;
    bool completed;
    bool finallyCompleted;
    float timer = 120;
    bool winGame;

    public GameObject winnerPanel;
    public GameObject loserPanel;
    public Text timeToResetGameText;
    public Text waitToReplay;
    float resetTimer = 5;

    [SerializeField]
    private int TeddyBearPointsMin = 2000;

    [SerializeField]
    private GameObject TeddyBear;

    [SerializeField]
    private TextMeshPro plinkoScore;
    [SerializeField]
    private TextMeshPro wheelScore;
    [SerializeField]
    private TextMeshPro coinScore;

    public static CarnivalScores Instance;

    private int plinkoPoints;
    private int wheelPoints;
    private int coinPoints;



    void Awake()
    {

        if (Instance == null)
            Instance = this;
        TeddyBear.SetActive(false);
        winnerPanel.SetActive(false);
        loserPanel.SetActive(false);
        waitToReplay.enabled = false;
        timeToResetGameText.enabled = false;
        waitToReplay.enabled = false;
    }

    void OnDestroy()
    {
        if (Instance = this)
            Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes1, seconds1;
        string niceTime1;


        plinkoScore.text = "Plinko: " + plinkoPoints.ToString("0000");
        wheelScore.text = "Wheel: " + wheelPoints.ToString("0000");
        coinScore.text = "Coins: " + coinPoints.ToString("0000");

        if ((plinkoPoints + wheelPoints + coinPoints >= TeddyBearPointsMin) && (timer > 0))
        {
            //TeddyBear.transform.position = camera.transform.forward + new Vector3(0, 5, 0);
            //TeddyBear.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles);
            TeddyBear.SetActive(true);
            winnerPanel.transform.position = camera.transform.forward + new Vector3(0, 5, 0);
            winnerPanel.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles);
            winnerPanel.SetActive(true);

            waitToReplay.enabled = true;
            timeToResetGameText.enabled = true;
            waitToReplay.enabled = true;

            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
                resetTimer = 0;
            minutes1 = Mathf.FloorToInt(resetTimer / 60F);
            seconds1 = Mathf.FloorToInt(resetTimer - minutes1 * 60);
            niceTime1 = string.Format("{0:0}:{1:00}", minutes1, seconds1);
            timeToResetGameText.text = niceTime1;

            StartCoroutine(TimeToReplay());
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Carnival");
            }
        }
        else if (timer <= 0)
        {
            loserPanel.transform.position = camera.transform.forward + new Vector3(0, 5, 0);
            loserPanel.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles);
            loserPanel.SetActive(true);

            waitToReplay.enabled = true;
            timeToResetGameText.enabled = true;
            waitToReplay.enabled = true;

            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
                resetTimer = 0;
            minutes1 = Mathf.FloorToInt(resetTimer / 60F);
            seconds1 = Mathf.FloorToInt(resetTimer - minutes1 * 60);
            niceTime1 = string.Format("{0:0}:{1:00}", minutes1, seconds1);
            timeToResetGameText.text = niceTime1;

            StartCoroutine(TimeToReplay());

            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Carnival");
            }
        }       

        timer -= Time.deltaTime;
        if (timer <= 0)
            timer = 0;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeRemaninginText.text = niceTime;
    }

    public void IncrementPlinkoScore(float points)
    {
        plinkoPoints += (int)points;
    }

    public void IncrementWheelScore(float points)
    {
        wheelPoints += (int)points;
    }

    public void IncrementCoinScore()
    {
        coinPoints += 1000;
    }


    IEnumerator TimeToReplay()
    {
        yield return new WaitForSeconds(5);
    }
}
