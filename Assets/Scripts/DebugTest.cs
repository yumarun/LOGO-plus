using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DebugTest : MonoBehaviour
{
    [SerializeField] Text SessionText;
    [SerializeField] Text OriginText;
    [SerializeField] Text DebugText;
    [SerializeField] Text SphereText;

    [SerializeField] GameObject aRSession;
    [SerializeField] GameObject aRSessionOrigin;
    [SerializeField] ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] GameObject redCubePref;
    [SerializeField] GameObject sphere;

    Vector3 trackedImgVec = new Vector3(-1, -1, -1);
    GameObject nRedObj;


    bool existedObj = false;

    void Start()
    {
        /*
        OriginText.text = aRSessionOrigin.transform.position.ToString();
        SessionText.text = aRSession.transform.position.ToString();

        DebugText.text = "non";

        aRTrackedImageManager.trackedImagesChanged += OnChanged1;
        */
    }

    void Update()
    {
        /*
        GameObject nObj = GameObject.Find("Cube");
        if (nObj != null)
        {
            OriginText.text = "ok "+ Vector3.Distance(nObj.transform.position, aRSessionOrigin.transform.position).ToString();
            SessionText.text = "ok " + Vector3.Distance(nObj.transform.position, aRSession.transform.position).ToString();
            Debug.Log("no");
        }
        else
        {
            Debug.Log("yes");
        }
        */

        /*
        DebugText.text = aRSessionOrigin.transform.rotation.ToString() + "\n";
        DebugText.text += aRSessionOrigin.transform.GetChild(0).localRotation.ToString() + "\n";
        DebugText.text += aRSessionOrigin.transform.GetChild(0).rotation.ToString() + "\n";
        DebugText.text += "aiueo\n";
        DebugText.text += aRSessionOrigin.transform.position.ToString() + "\n";
        DebugText.text += aRSessionOrigin.transform.GetChild(0).transform.position.ToString() + "\n";
        DebugText.text += aRSession.transform.position.ToString() + "\n";

        SphereText.text = sphere.transform.position.ToString();
        */
    }

    void OnChanged1(ARTrackedImagesChangedEventArgs eventArgs)
    {
        DebugText.text = "mitsuketa0";
        foreach(var trackedImage in eventArgs.added)
        {
            DebugText.text = eventArgs.added.Count.ToString() + " m_add " + trackedImage.transform.position.ToString();
        }
        foreach(var trackedImage in eventArgs.updated)
        {
            DebugText.text = eventArgs.updated.Count.ToString()+" m_update " + trackedImage.transform.position.ToString();
            if (!existedObj)
            {
                nRedObj = Instantiate(redCubePref) as GameObject;
                nRedObj.transform.position = trackedImage.transform.position;
                nRedObj.transform.localRotation = trackedImage.transform.localRotation;
                trackedImgVec = trackedImage.transform.position;
                
                existedObj = true;
            }
            
        }
    }

    public void OnComeButton()
    {
        nRedObj.transform.position -= trackedImgVec.normalized * 0.1f;
    }

    public void OnFarButton()
    {
        nRedObj.transform.position += trackedImgVec.normalized * 0.1f;
    }
}
