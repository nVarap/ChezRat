using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenPizzaController : MonoBehaviour
{
    public GameObject helpText;
    public LayerMask playerLayer;
    public PizzaTypes pizza;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            helpText.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && (playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            other.GetComponent<PlayerController>().pizzasBeingCarried.Add(pizza);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
        {
            helpText.SetActive(false);
        }
    }
}
public enum PizzaTypes
{
    Cheese, Pepperoni, FrozenCheese, FrozenPepperoni
}