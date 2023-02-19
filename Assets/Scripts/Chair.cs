using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Direction direction;
    public ChairState chairState;

    private Vector3 initialPos;

    public float outShift = 0.4f;
    public float pullSpeed = 0.00f;
    public float sitDist = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (chairState)
        {
            case ChairState.Pull:
                pull();
                break;
            case ChairState.Push:
                this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, initialPos, pullSpeed);
                break;
            case ChairState.Sit:
                sit();
                break;
            default:
                break;
        }
    }
    void sit()
    {
        Debug.Log(Vector3.Distance(this.gameObject.transform.localPosition, initialPos));
        Debug.Log(outShift - sitDist);
        if (Vector3.Distance(this.gameObject.transform.localPosition, initialPos) < (outShift - sitDist))
        {
            pull();
        }
        else
        {
            chairState = ChairState.Push;
        }
    }

    void pull()
    {
        this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(outShift, 0, 0) + initialPos, pullSpeed);
        // switch (direction)
        // {
        //     case Direction.Top:
        //         this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(outShift, 0, 0) + initialPos, pullSpeed);
        //         break;
        //     case Direction.Left:
        //         this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(0, 0, outShift) + initialPos, pullSpeed);

        //         break;
        //     case Direction.Down:
        //         this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(-outShift, 0, 0) + initialPos, pullSpeed);

        //         break;
        //     case Direction.Right:
        //         this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(0, 0, -outShift) + initialPos, pullSpeed);

        //         break;
        //     default:
        //         break;
        // }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if ((playerLayer.value & 1 << other.gameObject.layer) > 0)
    //     {
    //         Debug.Log("yes");
    //     }
    // }
}
public enum Direction
{
    Top, Down, Left, Right
}
public enum ChairState
{
    Push, Pull, Sit
}