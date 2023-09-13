using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sv_RailFadeAnimation : MonoBehaviour
{
    MeshRenderer[] _meshRenderers = new MeshRenderer[40];
    float _span = 0.36f;


    void Start()
    {
        Transform tf = gameObject.transform;
        for (var i = 0; i < 40; i++)
        {
            _meshRenderers[i] = tf.GetChild(i + 8).gameObject.GetComponent<MeshRenderer>();
        }
        for (var i = 8; i < 40; i++)
        {
            var color0 = _meshRenderers[i].materials[0].color;
            _meshRenderers[i].materials[0].color = new Color(color0.r, color0.g, color0.b, 0f);
            var color1 = _meshRenderers[i].materials[1].color;
            _meshRenderers[i].materials[1].color = new Color(color1.r, color1.g, color1.b, 0f);
        }
        StartCoroutine(RailsAnimationCoroutine());
    }

    IEnumerator RailsAnimationCoroutine()
    {
        int cnt = 0;
        float elapsedTime = 0;
        while (cnt < 40)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > _span)
            {

                elapsedTime = 0;
                cnt++;
            }

            FadeOutRail(cnt);
            PopUpRail((cnt + 8) % 40);

            yield return null;
        }

        
    }

    void PopUpRail(int idx)
    {
        _meshRenderers[idx].materials[0].color += new Color(0, 0, 0, Time.deltaTime / _span);
        _meshRenderers[idx].materials[1].color += new Color(0, 0, 0, Time.deltaTime / _span);
    }

    void FadeOutRail(int idx)
    {
        _meshRenderers[idx].materials[0].color -= new Color(0, 0, 0, Time.deltaTime / _span);
        _meshRenderers[idx].materials[1].color -= new Color(0, 0, 0, Time.deltaTime / _span);
    }
}
