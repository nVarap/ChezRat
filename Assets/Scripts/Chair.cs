using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Direction direction;
    [HideInInspector]
    public ChairState chairState;

    private Vector3 initialPos;

    public float outShift = 0.4f;
    public float pullSpeed = 0.00f;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.position;
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
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, initialPos, pullSpeed);
                break;
            default:
                break;
        }
    }
    void pull()
    {
        switch (direction)
        {
            case Direction.Top:
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(outShift, 0, 0) + initialPos, pullSpeed);
                break;
            case Direction.Left:
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(0, 0, outShift) + initialPos, pullSpeed);

                break;
            case Direction.Down:
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(-outShift, 0, 0) + initialPos, pullSpeed);

                break;
            case Direction.Right:
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(0, 0, -outShift) + initialPos, pullSpeed);

                break;
            default:
                break;
        }
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
    Push, Pull
}