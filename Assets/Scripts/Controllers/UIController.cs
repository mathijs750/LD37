using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverlayType
{
    Periscope,
    DepthMeter,
    ValvePuzzle,
    StatusScreen,
    ControllUnit,
    DigDeeper
}

public class UIController : MonoBehaviour
{
    private Dictionary<OverlayType, int> overlayTypes;

    void Awake()
    {
        overlayTypes = new Dictionary<OverlayType, int> { { OverlayType.Periscope, 0 },
            { OverlayType.DepthMeter, 1 }, { OverlayType.ValvePuzzle, 2 },
            { OverlayType.StatusScreen, 3 }, { OverlayType.ControllUnit, 4 },
             { OverlayType.DigDeeper, 5 }};
    }

    public void ShowOverlay(OverlayType type)
    {
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true); // back button
        transform.GetChild(overlayTypes[type]).gameObject.SetActive(true);
    }

    public void CloseActiveOverlay()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
