using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public LayerMask player;
    public bool startingScene = false;
    public bool triggered = true;
    public bool loaded = true;
    private bool started = false;
    public float timeRunning = 0.0f;
    private float timeStart = 0.0f;
    public int sceneInstance = 0;
    public TriggerType triggerType = TriggerType.Scene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startWait());
        triggered = startingScene;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(started);
        timeRunning = Time.time - timeStart;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((player.value & 1 << other.gameObject.layer) > 0 && started)
        {
            if (triggerType == TriggerType.Camera) sceneInstance = 1;


            this.triggered = true;
            this.loaded = false;
            timeStart = Time.time;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((player.value & 1 << other.gameObject.layer) > 0 && started)
        {
            if (triggerType == TriggerType.Camera) sceneInstance = -1;
            Debug.Log("triggered" + started);

            this.triggered = false;
            timeStart = Time.time;
        }
    }
    IEnumerator startWait()
    {
        yield return new WaitForSeconds(1);
        started = true;
    }
}
public enum TriggerType
{
    Camera, Scene
}

