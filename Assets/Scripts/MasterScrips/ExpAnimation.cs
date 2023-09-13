using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpAnimation : MonoBehaviour
{
    [SerializeField] GameObject CrackedPref1;
    [SerializeField] GameObject CrackedPref2;
    [SerializeField] GameObject ExpPref;
    [SerializeField] GameObject OriginalPref;

    //Animator expAnimator;
    [SerializeField] Camera arCamera;
    RaycastHit hit;

    float elapsedTimeFromLastTap = 10f;
    int tapCount = 0;
    bool isOriginal = true;

    void Start()
    {
        //expAnimator = ExpPref.GetComponent<Animator>();
    }

    void Update()
    {


        elapsedTimeFromLastTap += Time.deltaTime;


        if (elapsedTimeFromLastTap > 30f)
        {
            elapsedTimeFromLastTap = 10f;
        }
        if (elapsedTimeFromLastTap > 3f && MCController.CanIdolAnim)
        {
            tapCount = 0;
            // TODO: yaru
            //ChangePref(OriginalPref, true);
            
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
                            if (elapsedTimeFromLastTap < 2f)
                            {
                                tapCount++;
                            }

                            elapsedTimeFromLastTap = 0f;
                        }
                        

                    }
                }
                
            }
        }

        /*
        if (tapCount == 0 && !isOriginal)
        {
            isOriginal = true;
            ChangePref(OriginalPref);
        }
        */

        if (tapCount == 5)
        {
            isOriginal = false;
            ChangePref(CrackedPref1);
        }

        if (tapCount == 10)
        {
            isOriginal = false;
            ChangePref(CrackedPref2);
        }

        if (tapCount == 15)
        {
            isOriginal = false;
            ChangePref(ExpPref);
            // ExpPrefを新しいものにする
        }

        if (tapCount == 17)
        {
            isOriginal = false;
            Explosion();

        }
    }

    public void ChangePref(GameObject pref, bool _isOriginal = false)
    {
        if (MCController.MCObj == null)
        {
            return;
        }

        Destroy(MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject);
        GameObject newMObj = Instantiate(pref) as GameObject;
        newMObj.transform.SetParent(MCController.MCObj.transform.GetChild(0));
        if (_isOriginal)
        {
            newMObj.transform.localPosition = new Vector3(1f, -0.25f, 0);
        }
        else
        {
            newMObj.transform.localPosition = new Vector3(1f, 0, 0);
        }
        newMObj.transform.localRotation = Quaternion.Euler(0, 90, 0);
        newMObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        if (_isOriginal)
        {
            GameObject.Find("Manager").GetComponent<IdolAnimation>().MObj = newMObj;

        }
    }

    void Explosion()
    {
        Animator expAnimator = MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
        expAnimator.SetBool("GoExp", true);
    }
}
