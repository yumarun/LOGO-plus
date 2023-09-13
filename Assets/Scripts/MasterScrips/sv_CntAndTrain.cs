using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sv_CntAndTrain : MonoBehaviour
{
    [SerializeField] Camera _arCamera;

    [SerializeField] GameObject _SV1;
    [SerializeField] GameObject _SV2;
    [SerializeField] GameObject _SV3;
    [SerializeField] GameObject _SV4;
    [SerializeField] GameObject _SV5;
    [SerializeField] GameObject _SV6;
    [SerializeField] GameObject _SV7;
    [SerializeField] GameObject _SVTrain;

    [SerializeField] Text _debugText;

    RaycastHit _hit;
    bool _touchingSV;
    float _elapsedTime = 0f;
    Vector3 _prePos = new Vector3();
    public static int _prefCond = 7;

    void Start()
    {
        _prefCond = 7;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];


            if (touch.phase == TouchPhase.Began)
            {
                var ray = _arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out _hit))
                {
                    if (_hit.transform.gameObject.tag == "o_seven")
                    {
                        _touchingSV = true;
                    }

                }
            }
            else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && _touchingSV)
            {
                if (Vector3.Distance(touch.position, _prePos) < 14.37395f)
                {
                    _elapsedTime += Time.deltaTime;
                }

                this._prePos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ResetCond();
                _touchingSV = false;
            }

            ChangeLogo();
        }

        //_debugText.text = _elapsedTime.ToString();
    }

    void ResetCond()
    {
        _elapsedTime = 0f;

    }

    void ChangeLogo()
    {
        if (1f < _elapsedTime && _elapsedTime < 2f && _prefCond != 6)
        {
            //to 6
            _prefCond = 6;
            GameObject nSV = Instantiate(_SV6) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0, 0);
            nSV.transform.localRotation = Quaternion.Euler(0, -90, -10);
            nSV.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (2f < _elapsedTime && _elapsedTime < 3f && _prefCond != 5)
        {
            _prefCond = 5;
            GameObject nSV = Instantiate(_SV5) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(-15, 0, -1);
            nSV.transform.localRotation = Quaternion.Euler(0, -90, -20);
            nSV.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (3f < _elapsedTime && _elapsedTime < 4f && _prefCond != 4)
        {
            _prefCond = 4;
            GameObject nSV = Instantiate(_SV4) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0, -1);
            nSV.transform.localRotation = Quaternion.Euler(0, 275, -20);
            nSV.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (4f < _elapsedTime && _elapsedTime < 5f && _prefCond != 3)
        {
            _prefCond = 3;
            GameObject nSV = Instantiate(_SV3) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(-2.65f, -3.5f, -1f);
            nSV.transform.localRotation = Quaternion.Euler(-19.5f, 184, 0);
            nSV.transform.localScale = new Vector3(1000, 1000, 1000);
        }
        else if (5f < _elapsedTime && _elapsedTime < 6f && _prefCond != 2)
        {
            _prefCond = 2;
            GameObject nSV = Instantiate(_SV2) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0, 0);
            nSV.transform.localRotation = Quaternion.Euler(-20, 180, 0);
            nSV.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (6f < _elapsedTime && _elapsedTime < 7f && _prefCond != 1)
        {
            _prefCond = 1;
            GameObject nSV = Instantiate(_SV1) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0.5f, -1f);
            nSV.transform.localRotation = Quaternion.Euler(0, -90, -20);
            nSV.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (7f < _elapsedTime && _prefCond != 0)
        {
            _prefCond = 0;
            GameObject nSV = Instantiate(_SVTrain) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0, 0);
            nSV.transform.localRotation = Quaternion.Euler(-20, 180, 0);
            nSV.transform.localScale = new Vector3(2, 2, 2);

            var anim = nSV.GetComponent<Animator>();
            anim.SetBool("GoTrain", true);

            //StartCoroutine(StartTrainAnimationCoroutine(nSV));

        }
        else if (_elapsedTime < 0.001f && _prefCond != 7 && _prefCond != 0)
        {
            _prefCond = 7;
            GameObject nSV = Instantiate(_SV7) as GameObject;
            Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
            nSV.transform.SetParent(MCController.SVObj.transform);
            nSV.transform.SetSiblingIndex(1);
            nSV.transform.localPosition = new Vector3(0, 0, 0);
            nSV.transform.localRotation = Quaternion.Euler(-20, 180, 0);
            nSV.transform.localScale = new Vector3(2, 2, 2);

        } 
    }

    
}
