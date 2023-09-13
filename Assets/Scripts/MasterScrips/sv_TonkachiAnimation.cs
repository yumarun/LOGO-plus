using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sv_TonkachiAnimation : MonoBehaviour
{
    [SerializeField] Camera _arCamera;
    [SerializeField] GameObject _SVTonkachiPref;
    [SerializeField] Text _debugText;

    RaycastHit _hit;
    Vector2 _touchStartPos = new Vector2(0f, 0f);
    Vector2 _touchEndPos = new Vector2(0f, 0f);
    bool _touchingSV = false;

    public enum FlickDirection
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        TAP
    }
    FlickDirection _flickDirection = FlickDirection.NONE;


    int _cnt = 0;

    void Update()
    {
        if (MCController.mode == MCController.Mode.PLAY)
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
                            _touchStartPos = touch.position;
                            _touchingSV = true;
                        }

                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _touchEndPos = touch.position;
                    _flickDirection = GetDirection();
                    if (_touchingSV && _flickDirection == FlickDirection.UP)
                    {
                        _cnt++;
                        //_debugText.text = _cnt.ToString();
                        StartTonkachiAnimation();
                    }
                    
                    _touchingSV = false;
                }
            }
        }
        //_debugText.text = $"{_touchingSV}, {_flickDirection}";

    }

    FlickDirection GetDirection()
    {
        float dirX = _touchEndPos.x - _touchStartPos.x;
        float dirY = _touchEndPos.y - _touchStartPos.y;
        FlickDirection ret = FlickDirection.NONE;

        if (Mathf.Abs(dirY) < Mathf.Abs(dirX))
        {
            if (30 < dirX)
            {
                ret = FlickDirection.RIGHT;
            }
            else if (dirX < -30)
            {
                ret = FlickDirection.LEFT;
            }
        }
        else if (Mathf.Abs(dirY) > Mathf.Abs(dirX))
        {
            if (30 < dirY)
            {
                ret = FlickDirection.UP;
            }
            else if (dirY < -30)
            {
                ret = FlickDirection.DOWN;
            }
        }
        else
        {
            ret = FlickDirection.TAP;
        }

        return ret;
    }

    void StartTonkachiAnimation()
    {
        sv_CntAndTrain._prefCond = 0;
        GameObject nSV = Instantiate(_SVTonkachiPref) as GameObject;
        Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
        nSV.transform.SetParent(MCController.SVObj.transform);
        nSV.transform.SetSiblingIndex(1);
        nSV.transform.localPosition = new Vector3(0, 0.39f, -1.08f);
        nSV.transform.localRotation = Quaternion.Euler(0, -90, -20);
        nSV.transform.localScale = new Vector3(2, 2, 2);

        var anim = nSV.GetComponent<Animator>();
        anim.SetBool("GoTonkachi", true);
    }
}
