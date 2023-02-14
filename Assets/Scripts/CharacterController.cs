using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    public bool rotate = true;
    public float speed = 5;
    public float maxSpeedMod = 1.2f;
    public float friction = 1f;
    private GameObject player;
    private Rigidbody rb;



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
        movement = speed * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move();
    }
    void move()
    {
        if (rotate)
        {
            Debug.Log((rb.velocity - new Vector3(friction, 0, friction)).sqrMagnitude);
            Debug.Log("velocity: " + rb.velocity);
            if (movement.sqrMagnitude > 0.1 && new Vector3(rb.velocity.x, 0, rb.velocity.y).sqrMagnitude < speed * speed * maxSpeedMod)
            {
                rb.AddForce(Quaternion.Euler(0, 45, 0) * movement);

            }
            else if (movement.sqrMagnitude < 0.9)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Min(Mathf.Abs(rb.velocity.magnitude - friction), 0));

            }

        }
        else
        {
            rb.velocity = speed * movement;

        }
    }
}
