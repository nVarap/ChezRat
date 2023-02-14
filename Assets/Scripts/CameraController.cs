using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyCode switchKey = KeyCode.Space;
    public List<CameraPositions> cameraPositions;
    public Camera cam;
    [HideInInspector]
    public int currentIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentIndex += 1;
            if (currentIndex == cameraPositions.Count)
            {
                currentIndex = 0;
            }
        }
        if (cam.transform.position != cameraPositions[currentIndex].obj.transform.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPositions[currentIndex].obj.transform.position, cameraPositions[currentIndex].lerpSpeed);

        }
        if (cam.transform.rotation != cameraPositions[currentIndex].obj.transform.rotation)
        {
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, cameraPositions[currentIndex].obj.transform.rotation, cameraPositions[currentIndex].lerpSpeed);

        }
    }
}

[System.Serializable]
public struct CameraPositions
{
    public GameObject obj;
    [Range(0, 1)]
    public float lerpSpeed;
    public CameraTypes camTypes;

}
public enum CameraTypes
{
    Isographic,
    Forward,
}