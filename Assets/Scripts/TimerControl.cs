using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    [SerializeField]
    public int timeInSeconds = 360;
    public Text timer; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string convertSectoMin() {
        int mins = (timeInSeconds / 60);
        int secs = (timeInSeconds - mins) / 60;

        return mins.ToString("0") + ":" + secs.ToString("0");

    }
}
