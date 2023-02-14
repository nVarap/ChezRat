using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    public bool rotate = true;
    public float speed = 5;
    public float acceleration = 5;
    public float friction = 1f;
    public float frictionMultiplier = 0.2f;
    private GameObject player;
    private Rigidbody rb;
    private bool isMoving;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        move();
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
            movement = speed * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = Quaternion.Euler(0, 45, 0) * movement;

            Vector3 currentVel = rb.velocity;
            Vector3 velChange = movement - currentVel;
            velChange.x = Mathf.Clamp(velChange.x, -acceleration, acceleration);
            velChange.y = 0;
            velChange.z = Mathf.Clamp(velChange.z, -acceleration, acceleration);
            rb.AddForce(velChange, ForceMode.VelocityChange);

        }
        else
        {
            Debug.Log("slowning");
            movement = Quaternion.Euler(0, 45, 0) * Vector3.zero;
            Vector3 currentVel = rb.velocity;
            Vector3 velChange = movement - currentVel;
            velChange.x = Mathf.Clamp(velChange.x, -friction, friction);
            velChange.y = 0;
            velChange.z = Mathf.Clamp(velChange.z, -friction, friction);
            rb.velocity += (velChange * frictionMultiplier);
        }
    }
}
