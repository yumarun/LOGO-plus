using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    [SerializeField] ToggleGroup toggleGroup;

    public void ChangeMode()
    {
        var toggles = toggleGroup.ActiveToggles();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.name == "ToggleINFO")
            {
                MCController.mode = MCController.Mode.INFO;
            }
            else if (toggle.name == "TogglePLAY")
            {
                MCController.mode = MCController.Mode.PLAY;
            }
        }
    }
}
