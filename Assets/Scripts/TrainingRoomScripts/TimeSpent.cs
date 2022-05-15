using UnityEngine;
using UnityEngine.UI;

public class TimeSpent : MonoBehaviour {
    int init;

    //10 last times
    public int[] playerTimes;
    int timeInt;

    public bool timeIsRunning;

    float timeSinceBeginning;
    Text timeSpentText;
    string timeToShow;

    void Start() {
        resetTime();
        timeSpentText = GetComponent<Text>();
        playerTimes = new int[10];
    }

    void Update() {
        manageTime();
    }

    //managing the time
    void manageTime() {
        if (timeIsRunning) {
            timeInt = (int) (Time.time - timeSinceBeginning);

            timeToShow = formatTime(timeInt);

            timeSpentText.text = " " + timeToShow;
        }
    }

    //start time for the next route
    public void startTimer() {
        timeIsRunning = true;
    }

    //stop time and save the time in the top 10
    public void stopTimer() {
        timeIsRunning = false;
        if (init < 10) {
            playerTimes[init] = timeInt;
            init++;
        }
        else {
            init = 0;
            playerTimes[init] = timeInt;
        }
    }

    //format time into minutes and seconds
    public string formatTime(int time) {
        var minutes = Mathf.FloorToInt(time / 60F);
        var seconds = Mathf.FloorToInt(time - minutes * 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void resetTime() {
        timeSinceBeginning = Time.time;
    }
}