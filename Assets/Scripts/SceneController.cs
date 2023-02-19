using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SceneInstance[] sceneInstances;
    public CameraPos[] cameraPos;


    public Vector3 unloadShiftDist = new Vector3(0, -40, 0);
    public float loadSpeed = 0.05f;
    public float snapDist = 0.01f;
    private List<Vector3> initialPositions = new List<Vector3>();
    public Camera cam;
    public int currentIndex;
    private int loadedScenes = 1;
    private Dictionary<SceneTrigger, CameraPositions> cameraPositions = new Dictionary<SceneTrigger, CameraPositions>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (CameraPos pos in cameraPos)
        {
            cameraPositions.Add(pos.trigger, pos.pos);
        }
        foreach (SceneInstance instance in sceneInstances)
        {
            initialPositions.Add(instance.scene.transform.position);
        }
        for (int i = 1; i < sceneInstances.Length; i++)
        {
            sceneInstances[i].scene.transform.position = sceneInstances[i].scene.transform.position + unloadShiftDist;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sceneInstances.Length; i++)
        {
            if (cameraPos[i].trigger.sceneInstance != 0)
            {
                loadedScenes += cameraPos[i].trigger.sceneInstance;
                cameraPos[i].trigger.sceneInstance = 0;
            }
            if (cameraPos[i].trigger.triggered && cameraPos[i].trigger.triggerType == TriggerType.Camera)
            {
                if (loadedScenes == 1)
                {
                    if (cam.transform.position != cameraPositions[cameraPos[i].trigger].obj.transform.position)
                    {
                        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPositions[cameraPos[i].trigger].obj.transform.position, cameraPositions[cameraPos[i].trigger].lerpSpeed);

                    }
                    if (cam.transform.rotation != cameraPositions[cameraPos[i].trigger].obj.transform.rotation)
                    {
                        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, cameraPositions[cameraPos[i].trigger].obj.transform.rotation, cameraPositions[cameraPos[i].trigger].lerpSpeed);

                    }
                }
            }
            if (!sceneInstances[i].trigger.loaded)
            {
                Debug.Log('a');
                StartCoroutine(loadScene(sceneInstances[i].scene, i));
                sceneInstances[i].trigger.loaded = true;
            }
            if (sceneInstances[i].trigger.loaded && !sceneInstances[i].trigger.triggered && Vector3.Distance(sceneInstances[i].scene.transform.position, initialPositions[i]) < unloadShiftDist.magnitude - snapDist)
            {
                Debug.Log("a" + !sceneInstances[i].trigger.triggered);

                sceneInstances[i].scene.transform.position = Vector3.Lerp(initialPositions[i], initialPositions[i] + unloadShiftDist, loadingGraph(sceneInstances[i].trigger.timeRunning));
            }
        }

    }
    public IEnumerator loadScene(GameObject scene, int index)
    {
        while (Vector3.Distance(scene.transform.position, initialPositions[index]) > snapDist)
        {
            scene.transform.position = Vector3.Lerp(scene.transform.position, initialPositions[index], loadSpeed);
            yield return null;
        }
        scene.transform.position = initialPositions[index];
    }
    float loadingGraph(float x)
    {
        return 2 / (1 + Mathf.Pow((float)System.Math.E, -(-1 / (x + 0.1f))));
        // return 2 / (1 + Mathf.Pow((float)System.Math.E, -(Mathf.Pow(7, x - 1.8f)))) - 1;
    }
}
[System.Serializable]
public struct SceneInstance
{
    public SceneTrigger trigger;
    public GameObject scene;
}
[System.Serializable]
public struct CameraPos
{
    public SceneTrigger trigger;
    public CameraPositions pos;
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