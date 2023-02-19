using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBounce : MonoBehaviour
{
    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = initialPos + new Vector3(0, 0.3f * Mathf.Sin(1.5f * Time.time), 0);
    }
}
