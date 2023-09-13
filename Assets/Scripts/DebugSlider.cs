using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DebugSlider : MonoBehaviour
{
    //[SerializeField] Slider slider;
    [SerializeField] ARSessionOrigin aRSessionOrigin;
    [SerializeField] ARSession aRSession;


    GameObject aRCamera;

    void Start()
    {
        aRCamera = aRSessionOrigin.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }

    public void OnSliderValueChanged(float value)
    {
        //var tmp = new Vector3(aRCamera.transform.rotation.x, aRCamera.transform.rotation.y, aRCamera.transform.rotation.z);
        //aRSession.transform.position += value * tmp;
        aRSessionOrigin.transform.position += new Vector3(value, value, value);
    }
}
