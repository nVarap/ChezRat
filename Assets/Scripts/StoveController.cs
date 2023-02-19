using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public PizzaTypes[] pizzas;
    public LayerMask playerlayer;
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
        // if ((playerLayer.value & 1 << other.gameObject.layer) > 0){

        // }
    }
}
