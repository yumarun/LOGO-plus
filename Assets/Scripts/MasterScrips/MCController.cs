using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class MCController : MonoBehaviour
{


    [SerializeField] ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] GameObject MCObjPref;
    [SerializeField] GameObject SVObjPref;
    [SerializeField] Text debugText;

    const float MRotationSpan = 10f;

    public enum Mode
    {
        PLAY,
        INFO
    }
    public static Mode mode;

    public static GameObject MCObj;
    public static GameObject SVObj;
    GameObject MObj;
    bool existedObj = false;
    bool existedSV = false;
    Vector3 trackedImgVec;
    float mCRecognizedTime;

    public static bool CanIdolAnim = true;

    void Start()
    {
        CanIdolAnim = true;
        mode = Mode.PLAY;
        aRTrackedImageManager.trackedImagesChanged += OnMCRecognized;
    }


    

    void OnMCRecognized(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            
            if (trackedImage.referenceImage.name == "WGUIlEvA_400x400")
            {
                if (!existedObj)
                {
                    MCObj = Instantiate(MCObjPref) as GameObject;
                    MCObj.transform.position = trackedImage.transform.position;
                    MCObj.transform.localRotation = trackedImage.transform.localRotation;
                    trackedImgVec = trackedImage.transform.position;
                    StartPopupAnimation();

                    existedObj = true;
                }
            }
            else if (trackedImage.referenceImage.name == "img_shopLogo")
            {
                if (!existedSV)
                {
                    SVObj = Instantiate(SVObjPref) as GameObject;
                    SVObj.transform.position = trackedImage.transform.position;
                    SVObj.transform.localPosition = trackedImage.transform.localPosition;

                    existedSV = true;
                }
            }

            

        }
    }

    void StartPopupAnimation()
    {
        MObj = MCObj.transform.GetChild(0).GetChild(0).gameObject;
        mCRecognizedTime = Time.time;
        StartCoroutine(MoveM());
    }


    float MStartMoveSpeed = 0.09f;

    IEnumerator MoveM()
    {
        while (MCObj.transform.localPosition.x < 1f - 0.0001f)
        {
            float distCoverd = (Time.time - mCRecognizedTime) * MStartMoveSpeed;
            float fracJourny = distCoverd / 1f;

            MObj.transform.localPosition = Vector3.Lerp(new Vector3(0, -0.25f, 0), new Vector3(1f, -0.25f, 0), fracJourny);
            yield return null;
        }
    }


}
