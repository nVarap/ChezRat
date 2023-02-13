using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void move()
    {

    }
}
