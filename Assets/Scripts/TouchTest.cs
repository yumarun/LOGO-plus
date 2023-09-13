using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour
{
    [SerializeField] GameObject popUpObjPref;
    [SerializeField] Text testText;

    [SerializeField] Camera arCamera;

    //ARRaycastManager raycastManager;
    //List<ARRaycastHit> hits = new List<ARRaycastHit>();
    RaycastHit hit;

    void Start()
    {
        //raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("touchCount_ok");
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("touchPhase_ok");
                var ray = arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("raycast_ok");
                    var popUpObj = Instantiate(popUpObjPref) as GameObject;
                    popUpObj.transform.position = hit.transform.position;
                    testText.text = "atatta";
                }
            }
        }
    }

    void OnTappedObj()
    {

    }
}
