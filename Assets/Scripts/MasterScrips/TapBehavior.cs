using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapBehavior : MonoBehaviour
{

    [SerializeField] Text debugText;

    public enum FlickDirection
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        TAP
    }
    FlickDirection flickDirection = FlickDirection.NONE;


    [SerializeField] Camera arCamera;


    RaycastHit hit;
    GameObject MObj;
    bool canControllM = true;
    bool tappedM = false;
    Vector2 touchStartPos = new Vector2(0f, 0f);
    Vector2 touchEndPos = new Vector2(0f, 0f); 

    void Update()
    {
        if (MCController.MCObj != null)
        {
            var anim = MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
            //debugText.text = anim.GetCurrentAnimatorStateInfo(0).
        }

        if (MCController.mode == MCController.Mode.PLAY)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    var ray = arCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.tag == "o_mac")
                        {
                            touchStartPos = touch.position;
                            tappedM = true;
                            OnTapped();
                        }
                        
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    touchEndPos = touch.position;
                    flickDirection = GetDirection();
                    if (tappedM && canControllM && flickDirection == FlickDirection.UP)
                    {
                        FlyingM();
                    }
                    tappedM = false;
                }
            }
        }



        
    }

    void OnTapped()
    {
        // Write Process when MObj pushed.
        //debugText.text += "OnTapped";
        StopCoroutine(IdolAnimation.ps_RotationCol);
        ResetMRotation();

    }

    void FlyingM()
    {
        MObj = MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject;
        var anim = MObj.GetComponent<Animator>();
        anim.SetBool("PushAndFly", !anim.GetBool("PushAndFly"));
        MCController.CanIdolAnim = false;
    }

    FlickDirection GetDirection()
    {
        float dirX = touchEndPos.x - touchStartPos.x;
        float dirY = touchEndPos.y - touchStartPos.y;
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

    void ResetMRotation()
    {
        if (MCController.MCObj != null)
        {
            GameObject tMObj = MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject;
            tMObj.transform.localRotation = Quaternion.Euler(0, 90f, 0);
        }
    }
}
