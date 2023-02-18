using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public LayerMask chairLayer;
    [HideInInspector]
    public bool sitting = false;
    public bool standing = false;
    private Transform saveState;

    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        saveState = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && sitting)
        {
            Debug.Log("Sitting");
            Transform seatObj = other.gameObject.transform.Find("SeatPosition");
            Debug.Log(Vector3.Distance(seatObj.transform.position, this.transform.position));
            saveState.position = this.transform.position;
            saveState.rotation = this.transform.rotation;
            this.transform.position = seatObj.transform.position;
            this.transform.rotation = seatObj.transform.rotation;
            sitting = false;
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Sit;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null && standing)
        {
            other.gameObject.GetComponent<Chair>().chairState = ChairState.Push;
            this.transform.position = saveState.position;
            this.transform.rotation = saveState.rotation;


            standing = false;
        }
    }
    public void Sit()
    {

        if (sitting == false)
        {
            prevPos = this.transform.position;
            sitting = true;
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
    public void Stand()
    {

        this.transform.position = prevPos;
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;


        standing = true;
    }
}