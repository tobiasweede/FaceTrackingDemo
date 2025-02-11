
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using TMPro;

public class FtManager : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer tongueBlendShape;
    public SkinnedMeshRenderer leftEyeExample;
    public SkinnedMeshRenderer rightEyeExample;

    public GameObject text;
    public Transform TextParent;

    private List<TMP_Text> texts = new List<TMP_Text>();

    private float[] blendShapeWeight = new float[72];

    private List<string> blendShapeList = new List<string>
    {
        "eyeLookDownLeft",
        "noseSneerLeft",
        "eyeLookInLeft",
        "browInnerUp",
        "browDownRight",
        "mouthClose",
        "mouthLowerDownRight",
        "jawOpen",
        "mouthUpperUpRight",
        "mouthShrugUpper",
        "mouthFunnel",
        "eyeLookInRight",
        "eyeLookDownRight",
        "noseSneerRight",
        "mouthRollUpper",
        "jawRight",
        "browDownLeft",
        "mouthShrugLower",
        "mouthRollLower",
        "mouthSmileLeft",
        "mouthPressLeft",
        "mouthSmileRight",
        "mouthPressRight",
        "mouthDimpleRight",
        "mouthLeft",
        "jawForward",
        "eyeSquintLeft",
        "mouthFrownLeft",
        "eyeBlinkLeft",
        "cheekSquintLeft",
        "browOuterUpLeft",
        "eyeLookUpLeft",
        "jawLeft",
        "mouthStretchLeft",
        "mouthPucker",
        "eyeLookUpRight",
        "browOuterUpRight",
        "cheekSquintRight",
        "eyeBlinkRight",
        "mouthUpperUpLeft",
        "mouthFrownRight",
        "eyeSquintRight",
        "mouthStretchRight",
        "cheekPuff",
        "eyeLookOutLeft",
        "eyeLookOutRight",
        "eyeWideRight",
        "eyeWideLeft",
        "mouthRight",
        "mouthDimpleLeft",
        "mouthLowerDownLeft",
        "tongueOut",
        "viseme_PP",
        "viseme_CH",
        "viseme_o",
        "viseme_O",
        "viseme_i",
        "viseme_I",
        "viseme_RR",
        "viseme_XX",
        "viseme_aa",
        "viseme_FF",
        "viseme_u",
        "viseme_U",
        "viseme_TH",
        "viseme_kk",
        "viseme_SS",
        "viseme_e",
        "viseme_DD",
        "viseme_E",
        "viseme_nn",
        "viseme_sil",
    };

    private int[] indexList = new int[72];
    private int tongueIndex;
    private int leftLookDownIndex;
    private int leftLookUpIndex;
    private int leftLookInIndex;
    private int leftLookOutIndex;

    private int rightLookDownIndex;
    private int rightLookUpIndex;
    private int rightLookInIndex;
    private int rightLookOutIndex;

    private PxrFaceTrackingInfo faceTrackingInfo;

    void Start()
    {
        for (int i = 0; i < indexList.Length; i++)
        {
            indexList[i] = skin.sharedMesh.GetBlendShapeIndex(blendShapeList[i]);
            GameObject textGO = GameObject.Instantiate(text, TextParent);
            texts.Add(textGO.GetComponent<TMP_Text>());
        }

        tongueIndex = tongueBlendShape.sharedMesh.GetBlendShapeIndex("tongueOut");
        leftLookDownIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookDownLeft");
        leftLookUpIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookUpLeft");
        leftLookInIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookInLeft");
        leftLookOutIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookOutLeft");
        rightLookDownIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookDownRight");
        rightLookUpIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookUpRight");
        rightLookInIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookInRight");
        rightLookOutIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookOutRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (PXR_Plugin.System.UPxr_QueryDeviceAbilities(PxrDeviceAbilities.PxrTrackingModeFaceBit))
        {
            switch (PXR_Manager.Instance.trackingMode)
            {
                case FaceTrackingMode.Hybrid:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACELIP_DATA, ref faceTrackingInfo);
                    break;
                case FaceTrackingMode.FaceOnly:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACE_DATA, ref faceTrackingInfo);
                    break;
                case FaceTrackingMode.LipsyncOnly:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_LIP_DATA, ref faceTrackingInfo);
                    break;
            }
            blendShapeWeight = faceTrackingInfo.blendShapeWeight;
            float[] data = blendShapeWeight;
            for (int i = 0; i < data.Length; ++i)
            {
                texts[i].text = $"{blendShapeList[i]}\n{(int)(data[i] * 120)}";

                if (indexList[i] >= 0)
                {
                    skin.SetBlendShapeWeight(indexList[i], 100 * data[i]);
                }
            }

            tongueBlendShape.SetBlendShapeWeight(tongueIndex, 100 * data[51]);

            leftEyeExample.SetBlendShapeWeight(leftLookUpIndex, 100 * data[31]);
            leftEyeExample.SetBlendShapeWeight(leftLookDownIndex, 100 * data[0]);
            leftEyeExample.SetBlendShapeWeight(leftLookInIndex, 100 * data[2]);
            leftEyeExample.SetBlendShapeWeight(leftLookOutIndex, 100 * data[44]);
            rightEyeExample.SetBlendShapeWeight(rightLookUpIndex, 100 * data[35]);
            rightEyeExample.SetBlendShapeWeight(rightLookDownIndex, 100 * data[12]);
            rightEyeExample.SetBlendShapeWeight(rightLookInIndex, 100 * data[11]);
            rightEyeExample.SetBlendShapeWeight(rightLookOutIndex, 100 * data[45]);

        }
    }

    public float[] GetBlendshapeWeights()
    {
        return blendShapeWeight;
    }

    public void ToggleDebugUI()
    {
        TextParent.gameObject.SetActive(!TextParent.gameObject.activeSelf);
    }
}

