using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public LayerMask chairLayer;
    [HideInInspector]
    public bool sitting = false;
    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && sitting)
        {
            Debug.Log(this.GetComponent<BoxCollider>().enabled);

            this.GetComponent<BoxCollider>().enabled = false;
            Transform setObj = other.gameObject.transform.Find("SeatPosition");
            this.transform.position = setObj.transform.position;
            sitting = false;
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Pull;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && sitting)
        {
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Push;
        }
    }
    public void Sit()
    {
        Debug.Log(this.GetComponent<BoxCollider>().enabled);

        if (sitting == false)
        {
            prevPos = this.transform.position;
            sitting = true;

        }
    }
    public void Stand()
    {
        this.transform.position = prevPos;
        this.GetComponent<BoxCollider>().enabled = true;
    }
}