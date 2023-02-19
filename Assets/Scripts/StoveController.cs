using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public List<PizzaTypes> pizzas;
    public GameObject text;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            text.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.GetComponent<PlayerController>().pizzasBeingCarried.Count > 0)
                {
                    pizzas.Add(other.GetComponent<PlayerController>().pizzasBeingCarried[other.GetComponent<PlayerController>().pizzasBeingCarried.Count - 1]);
                    other.GetComponent<PlayerController>().pizzasBeingCarried.RemoveAt(other.GetComponent<PlayerController>().pizzasBeingCarried.Count - 1);
                }
            }
        }
    }
}
