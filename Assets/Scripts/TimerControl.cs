using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TimerControl : MonoBehaviour
{
    [SerializeField]
    public int timeInSeconds = 360;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI day;

    float time;
    int dayCount = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        time = timeInSeconds;
        timer.text = convertToTimer(timeInSeconds);
        day.text = "Day " + dayCount.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        timer.text = convertToTimer(time);
        if (time <= 0) {
            time = timeInSeconds;
            dayCount++;
            day.text = "Day " + dayCount.ToString();

        }
    }


    private string convertToTimer(float time) {
        float mins = Mathf.Floor(time / 60);
        float secs = Mathf.RoundToInt(time - (mins*60));
        if (secs < 10)
        {
            return mins.ToString() + ":" + "0" + secs.ToString();
        }
        else {
            return mins.ToString() + ":" + secs.ToString();
        }
        

    }
}
