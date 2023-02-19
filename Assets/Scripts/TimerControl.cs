using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TimerControl : MonoBehaviour
{
    [SerializeField]
    public float seconds = 60f;
    public float minutes = 5f;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI day;
    
    int dayCount = 1;
    
    

    // Start is called before the first frame update
    void Start()
    {
        timer.text = convertToTimer();
        day.text = "Day " + dayCount.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seconds -= Time.deltaTime;
        timer.text = convertToTimer();
        if (minutes == 0 && seconds == 0)
        {
            dayCount++;
            day.text = "Day " + dayCount.ToString();
        }
        if (seconds <= 0) { minutes -= 1; seconds = 60; Debug.Log("minute passed"); }
    }
    private string convertToTimer() {
        if (seconds < 10)
        {
            return minutes.ToString() + ":" + "0" + Mathf.FloorToInt(seconds).ToString();
        }
        else {
            return minutes.ToString() + ":" + Mathf.FloorToInt(seconds).ToString();
        }
        

    }

    
}
