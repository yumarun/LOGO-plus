using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    [SerializeField] GameObject _infoBlockPref;
    [SerializeField] Camera _arCamera;
    [SerializeField] string[] _tags;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Vector3[] _localPoss;
    [SerializeField] Vector3[] _localRots;
    [SerializeField] Vector3[] _localScls;

    RaycastHit _hit;
    Dictionary<string, int> _tagIndexPairs = new Dictionary<string, int>();
    List<bool> _existingInfoBlocks = new List<bool>();

    void Start()
    {
        

        for (int i = 0; i < _tags.Length; i++)
        {
            _tagIndexPairs[_tags[i]] = i;
            _existingInfoBlocks.Add(false);
        }
    }

    void Update()
    {
        if (MCController.mode == MCController.Mode.INFO)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    var ray = _arCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out _hit))
                    {
                        if (!_tagIndexPairs.ContainsKey(_hit.transform.gameObject.tag))
                        {
                            return;
                        }

                        int idx = _tagIndexPairs[_hit.transform.gameObject.tag];

                        if (!_existingInfoBlocks[idx])
                        {
                            PopUpInfoBlock(idx);

                        }
                        else
                        {
                            DestroyInfoBlock(idx);
                        }

                    }
                }
            }
        }
    }


    // TODO: erase many "if" statement.
    void PopUpInfoBlock(int idx)
    {
        GameObject infoBlock = Instantiate(_infoBlockPref) as GameObject;
        infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _sprites[idx];

        if (idx == 0)
        {
            //Destroy(MCController.MCObj.transform.GetChild(1).gameObject);
            infoBlock.transform.SetParent(MCController.MCObj.transform);
            infoBlock.transform.SetSiblingIndex(1);
            infoBlock.transform.localPosition = _localPoss[idx];
            infoBlock.transform.localRotation = Quaternion.Euler(_localRots[idx]);
            infoBlock.transform.localScale = _localScls[idx];
            _existingInfoBlocks[idx] = true;
        }
        else if (idx == 1)
        {
            //Destroy(MCController.SVObj.transform.GetChild(2).gameObject);
            infoBlock.transform.SetParent(MCController.SVObj.transform);
            infoBlock.transform.SetSiblingIndex(2);
            infoBlock.transform.localPosition = _localPoss[idx];
            infoBlock.transform.localRotation = Quaternion.Euler(_localRots[idx]);
            infoBlock.transform.localScale = _localScls[idx];
            _existingInfoBlocks[idx] = true;
        }
    }
    void DestroyInfoBlock(int idx)
    {
        if (idx == 0)
        {
            Destroy(MCController.MCObj.transform.GetChild(1).gameObject);
        }
        else if (idx == 1)
        {
            Destroy(MCController.SVObj.transform.GetChild(2).gameObject);
        }

        _existingInfoBlocks[idx] = false;
    }

}
