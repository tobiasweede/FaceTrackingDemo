using Unity.XR.PXR;
using UnityEngine;

public class FTTest : MonoBehaviour
{
    FtManager ftManager;
    FileWriter fileWriter;
    void Start()
    {
        ftManager = GetComponent<FtManager>();
        fileWriter = GetComponent<FileWriter>();

        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
        InvokeRepeating("SendValues", 0.0f, 1.0f);
    }

    void SendValues()
    {
        float[] blendShapeWeight = ftManager.GetBlendshapeWeights();
        string json = JsonUtility.ToJson(blendShapeWeight);
        Debug.Log(json);

    }

}

