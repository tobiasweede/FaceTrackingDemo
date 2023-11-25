using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendFaceState : MonoBehaviour
{

    public class Payload
    {
        public string Label { get; set; }
        public float[] BlendShapeWeight { get; set; }
    }

    void Start()
    {
        if (TryGetComponent<Button>(out Button b))
        {
            b.onClick.AddListener(Send);
        }
        else
        {
            Debug.Log("Button component not found!");
        }
    }

    public void Send()
    {
        Payload p = new Payload
        {
            Label = "Test",
            BlendShapeWeight = new float[72]
        };
        p.BlendShapeWeight[0] = 42.0f;
        StartCoroutine(ViaHttp(p));
    }



    IEnumerator ViaHttp(Payload p)
    {
        if (p.Label == null)
        {
            Debug.Log("Label missing");
            yield return 0;
        }
        if (p.BlendShapeWeight == null)
        {
            Debug.Log("blendShapeWeights missing");
            yield return 0;
        }


        var payload = JsonUtility.ToJson(p);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/api/v1/question", payload, "application/json"))
        {
            www.SetRequestHeader("token", "asdf");
            www.SetRequestHeader("accept", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
