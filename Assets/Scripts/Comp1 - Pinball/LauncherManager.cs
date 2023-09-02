using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LauncherManager : MonoBehaviour
{
    public static LauncherManager instance { get; private set; } //Singleton

    [Header("Ball")]
    [SerializeField] GameObject ball;
    Vector3 ballPos;

    [Header("launchPad")]
    [SerializeField] GameObject launcherPad;
    Vector3 launcherPos;
    float drawSpeed = 2f;
    float releaseSpeed = 10f;

    float m_pushStrength = 20f;

    [Header("Other")]
    [SerializeField] TextMeshProUGUI restartText;
    int restartCounter = 0;

    public bool keyPressed = false;
    public bool iActive = false;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timertext;
    public float timecounter = 0;
    public int second = 0;
    public int minute = 0;
    public int hour = 0;


    //--------------------


    private void Awake()
    {
        //Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        launcherPad.transform.position = new Vector3(5, 1.95f, -0.35f);
        launcherPos = launcherPad.transform.position;

        ballPos = ball.transform.position;
        restartText.text = "Restart: " + restartCounter;
    }
    private void Update()
    {
        LauchPadActivate();
        Timer();
    }


    //--------------------


    void LauchPadActivate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (launcherPad.transform.position.y > 0)
            {
                launcherPad.transform.position += -transform.up * Time.deltaTime * drawSpeed;
            }
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            if (launcherPad.transform.position.y < launcherPos.y)
            {
                launcherPad.transform.position += transform.up * Time.deltaTime * releaseSpeed;
            }
        }
    }
    public void Restart()
    {
        ball.transform.position = ballPos;

        restartCounter++;
        restartText.text = "Restart: " + restartCounter;
    }
    void Timer()
    {
        timecounter += Time.deltaTime;
        if (timecounter >= 1)
        {
            timecounter = 0;
            second += 1;

            if (second > 59)
            {
                second = 0;

                minute += 1;

                if (minute > 59)
                {
                    minute = 0;

                    hour += 1;
                }
            }

            timertext.text = "";

            if (hour < 10)
            {
                timertext.text += "0" + hour + ":";
            }
            else
            {
                timertext.text += hour + ":";
            }

            if (minute < 10)
            {
                timertext.text += "0" + minute + ":";
            }
            else
            {
                timertext.text += minute + ":";
            }

            if (second < 10)
            {
                timertext.text += "0" + second;
            }
            else
            {
                timertext.text += second;
            }
        }
    }
}
