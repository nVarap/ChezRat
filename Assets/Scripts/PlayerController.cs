using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public KeyCode chairKey = KeyCode.E;
    public LayerMask chairLayer;
    public float maxSpeed = 10f;
    public float acceleration = 5;
    public float friction = 1f;
    public float frictionMultiplier = 0.2f;
    public CameraController camController;
    private GameObject player;
    private Rigidbody rb;
    private bool isMoving;
    private bool chairSwitched = false;

    public List<PizzaTypes> pizzasBeingCarried = new List<PizzaTypes>();
    private Vector3 movement;
    public GameObject chef;
    public Animator anim;
    private bool prevMoving;
    private bool sitting = false;
    private bool standing = true;
    private Vector3 prevPos;
    private Transform saveState;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rb = this.GetComponent<Rigidbody>();
        saveState = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetKeyUp(chairKey))
        {
            chairSwitched = false;

        }
        if (Input.GetKeyDown(chairKey))
        {
            if (sitting)
            {
                Stand();
                sitting = false;
            }
        }

    }

    void move()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            anim.SetTrigger("Walk");
            Vector3 dir = Direction().normalized;
            Vector3 spid = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            float accel = acceleration;
            if (spid.magnitude > maxSpeed)
            {
                accel *= spid.magnitude / maxSpeed;
            }
            Vector3 dire = dir * maxSpeed - spid;

            if (dire.magnitude < 0.5f)
            {
                accel *= dire.magnitude / 0.5f;
            }
            dire = dire.normalized * accel;
            float magn = dire.magnitude;
            dire = dire.normalized;
            dire *= magn;
            rb.AddForce(dire);
            chef.transform.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(-90, Vector3.SignedAngle(new Vector3(0, 0, 1), Direction().normalized, Vector3.up), 0)), chef.transform.rotation, 0.75f);
        }
        else
        {
            if (camController.cameraPositions[camController.currentIndex].camTypes == CameraTypes.Isographic)
            {
                movement = Quaternion.Euler(0, 45, 0) * Vector3.zero;
            }
            else if (camController.cameraPositions[camController.currentIndex].camTypes == CameraTypes.Forward)
            {
                movement = Vector3.zero;

            }
            Vector3 currentVel = rb.velocity;
            Vector3 velChange = movement - currentVel;
            velChange.x = Mathf.Clamp(velChange.x, -friction, friction);
            velChange.y = 0;
            velChange.z = Mathf.Clamp(velChange.z, -friction, friction);
            rb.velocity += (velChange * frictionMultiplier);
        }
        if (!isMoving && prevMoving)
        {
            anim.SetTrigger("Walk");
            Debug.Log("aaahhhhh");

            anim.SetTrigger("Stop");
        }
        prevMoving = isMoving;
    }
    private Vector3 Direction()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hAxis, 0, vAxis);
        if (camController.cameraPositions[camController.currentIndex].camTypes == CameraTypes.Isographic)
        {
            return Quaternion.Euler(0, 45, 0) * direction;
        }
        else if (camController.cameraPositions[camController.currentIndex].camTypes == CameraTypes.Forward)
        {
            return direction;

        }
        return Quaternion.Euler(0, 45, 0) * direction;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(chairKey) && chairSwitched == false)
            if ((chairLayer.value & 1 << other.gameObject.layer) > 0 && other.gameObject.GetComponent<Chair>() != null)
            {

                if (other.gameObject.GetComponent<Chair>().chairState == ChairState.Push)
                {
                    Sit();
                    Transform seatObj = other.gameObject.transform.Find("SeatPosition");
                    saveState.position = this.transform.position;
                    this.transform.position = seatObj.transform.position;
                    chairSwitched = true;
                    other.gameObject.GetComponent<Chair>().chairState = ChairState.Pull;
                }
                else if (other.gameObject.GetComponent<Chair>().chairState == ChairState.Pull)
                {
                    chairSwitched = true;

                    other.gameObject.GetComponent<Chair>().chairState = ChairState.Push;
                }
            }
    }
    public void Sit()
    {

        if (sitting == false)
        {
            prevPos = this.transform.position;
            sitting = true;
            this.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
    public void Stand()
    {
        this.transform.position = prevPos;
        this.GetComponent<Rigidbody>().isKinematic = false;
        standing = true;
    }
}
