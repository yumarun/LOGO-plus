using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReceiver : MonoBehaviour
{
    [SerializeField] GameObject OriginalPref;
    [SerializeField] GameObject _SV7;

    public void ResetFlying()
    {
        var anim = GetComponent<Animator>();
        anim.SetBool("PushAndFly", false);
        MCController.CanIdolAnim = true;
    }

    public void ResetExplosion()
    {
        var anim = GetComponent<Animator>();
        anim.SetBool("GoExp", false);
        GameObject.Find("Manager").GetComponent<ExpAnimation>().ChangePref(OriginalPref, true);
        MCController.CanIdolAnim = true;
    }

    public void ResetTrain()
    {
        sv_CntAndTrain._prefCond = 7;
        GameObject nSV = Instantiate(_SV7) as GameObject;
        Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
        nSV.transform.SetParent(MCController.SVObj.transform);
        nSV.transform.SetSiblingIndex(1);
        nSV.transform.localPosition = new Vector3(0, 0, 0);
        nSV.transform.localRotation = Quaternion.Euler(-20, 180, 0);
        nSV.transform.localScale = new Vector3(2, 2, 2);
    }

    public void ResetTonkachi()
    {
        sv_CntAndTrain._prefCond = 7;
        GameObject nSV = Instantiate(_SV7) as GameObject;
        Destroy(MCController.SVObj.transform.GetChild(1).gameObject);
        nSV.transform.SetParent(MCController.SVObj.transform);
        nSV.transform.SetSiblingIndex(1);
        nSV.transform.localPosition = new Vector3(0, 0, 0);
        nSV.transform.localRotation = Quaternion.Euler(-20, 180, 0);
        nSV.transform.localScale = new Vector3(2, 2, 2);
    }
}
