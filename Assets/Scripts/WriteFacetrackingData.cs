using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;

public class WriteFacetrackingData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FileWriter w = gameObject.AddComponent<FileWriter>();
        Button b = GetComponent<Button>();
        FTTest f = GameObject.Find("FTManager").GetComponent<FTTest>();
        b.onClick.AddListener(Submit);
    }

    void Submit()
    {

    }
}
