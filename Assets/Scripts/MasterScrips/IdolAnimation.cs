using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdolAnimation : MonoBehaviour
{

    [SerializeField] Text debugText;
    [SerializeField] Text debugText2;

    const float rotateAnimationSpan = 10f;
    float elapsedTime = 0f;
    public GameObject MObj;


    public static Coroutine ps_RotationCol;

    
    void Update()
    {
        if (MCController.MCObj != null)
        {
            if (MObj == null)
            {
                MObj = MCController.MCObj.transform.GetChild(0).GetChild(0).gameObject;
            }
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > rotateAnimationSpan)
        {
            //debugText.text = Time.time.ToString() + " " + MCController.CanIdolAnim.ToString() + (MObj == null).ToString(); 
            elapsedTime = 0f;
            if (MCController.CanIdolAnim)
                RotationAnim();
        }

        
    }


    void RotationAnim()
    {
        ps_RotationCol = StartCoroutine(RotationCol());
    }

    

    IEnumerator RotationCol()
    {
        float t = 0f;
        //debugText.text += " RotCol";
        while (t < 4f)
        {
            t += Time.deltaTime;
            MObj.transform.Rotate(new Vector3(0, 200f, 0) * (t / 90f));
            yield return null;
        }

        while (t > 0.5f)
        {
            t -= Time.deltaTime;
            if (t < 3f && Mathf.Abs(MObj.transform.localRotation.eulerAngles.y - 90f) < 1f)
            {
                //debugText2.text = "Yes\n";
                MObj.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                break;
            }
            else
            {
                MObj.transform.Rotate(new Vector3(0, 200f, 0) * (t / 90f));
            }
            yield return null;
        }
    }
}
