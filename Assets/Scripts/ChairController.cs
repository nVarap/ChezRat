using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ChairController : MonoBehaviour
{
    public Chairs top, down, left, right;
    public float alterXPosMin, alterXPosMax;

    public float alterZPosMin, alterZPosMax;
    public bool refreshGO;
    public bool savePositions;
    public bool resetPositions;
    public float outShift = 0.05f;
    public float pullSpeed;


    private Vector3[] positions = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

    // Start is called before the first frame update
    void Start()
    {
        savePositions = true;
        refreshGO = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (savePositions)
        {
            if (positions.Length != 4)
            {
                positions = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
            }
            positions[0] = top.gameObject.transform.position;
            positions[1] = down.gameObject.transform.position;
            positions[2] = left.gameObject.transform.position;
            positions[3] = right.gameObject.transform.position;
            savePositions = false;
        }
        if (resetPositions)
        {
            top.gameObject.transform.position = positions[0];
            down.gameObject.transform.position = positions[1];
            left.gameObject.transform.position = positions[2];
            right.gameObject.transform.position = positions[3];
            resetPositions = false;
        }
        if (refreshGO)
        {
            top.gameObject.transform.position = positions[0] + new Vector3(Random.Range(alterXPosMin, alterXPosMax), 0, Random.Range(alterZPosMin, alterZPosMax));
            down.gameObject.transform.position = positions[1] + new Vector3(Random.Range(alterXPosMin, alterXPosMax), 0, Random.Range(alterZPosMin, alterZPosMax));
            left.gameObject.transform.position = positions[2] + new Vector3(Random.Range(alterXPosMin, alterXPosMax), 0, Random.Range(alterZPosMin, alterZPosMax));
            right.gameObject.transform.position = positions[3] + new Vector3(Random.Range(alterXPosMin, alterXPosMax), 0, Random.Range(alterZPosMin, alterZPosMax));

            refreshGO = false;
        }
        top.gameObject.SetActive(top.enabled);
        down.gameObject.SetActive(down.enabled);
        left.gameObject.SetActive(left.enabled);
        right.gameObject.SetActive(right.enabled);
        int index = 0;
        foreach (Chairs chair in new Chairs[] { top, down, left, right })
        {
            if (chair.pull)
            {
                switch (index)
                {
                    case 0:
                        chair.gameObject.transform.position = Vector3.Lerp(chair.gameObject.transform.position, new Vector3(outShift, 0, 0) + positions[index], pullSpeed);
                        break;
                    case 2:
                        chair.gameObject.transform.position = Vector3.Lerp(chair.gameObject.transform.position, new Vector3(0, 0, outShift) + positions[index], pullSpeed);

                        break;
                    case 1:
                        chair.gameObject.transform.position = Vector3.Lerp(chair.gameObject.transform.position, new Vector3(-0, 0, 0) + positions[index], pullSpeed);

                        break;
                    case 3:
                        chair.gameObject.transform.position = Vector3.Lerp(chair.gameObject.transform.position, new Vector3(0, 0, -outShift) + positions[index], pullSpeed);

                        break;
                    default:
                        break;
                }
            }
            else
            {
                chair.gameObject.transform.position = Vector3.Lerp(chair.gameObject.transform.position, positions[index], pullSpeed);
            }
            index += 1;
        }
    }
}
[System.Serializable]
public struct Chairs
{
    public GameObject gameObject;
    public bool enabled;
    public bool pull;
}