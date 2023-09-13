using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject _aRSession;

    public void OnResetButtonPushed()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        Destroy(GameObject.Find("AR Session"));
        yield return null;
        GameObject session = Instantiate(_aRSession) as GameObject;


    }
}
