using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendFaceState : MonoBehaviour
{
    private FTTest ftTest;

    void Start()
    {
        ftTest = GetComponent<FTTest>();
    }

    public void Send()
    {
        Debug.Log("Send");
        StartCoroutine(SendViaHttp());
    }

    IEnumerator SendViaHttp()
    {
        var payload = JsonUtility.ToJson(ftTest);

        using (UnityWebRequest www = UnityWebRequest.Post("https://127.0.0.1:8000/items", payload, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
