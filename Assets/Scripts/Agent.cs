using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public LayerMask chairLayer;
    [HideInInspector]
    public bool sitting = false;
    public bool standing = false;


    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.gameObject.layer);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && sitting)
        {
            Debug.Log("Sitting");
            Transform seatObj = other.gameObject.transform.Find("SeatPosition");
            this.transform.position = seatObj.transform.position;
            Debug.Log(Vector3.Distance(seatObj.transform.position, this.transform.position));
            this.transform.rotation = seatObj.transform.rotation;
            sitting = false;
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Pull;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && standing)
        {
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Push;
            standing = false;
        }
    }
    public void Sit()
    {

        if (sitting == false)
        {
            prevPos = this.transform.position;
            sitting = true;

        }
    }
    public void Stand()
    {

        this.transform.position = prevPos;

        standing = true;
    }
}