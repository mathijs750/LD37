using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayActivator : MonoBehaviour, IInteractable
{
    [SerializeField]
    private OverlayType type;

    public void Interact()
    {
        GameManager.instance.openOverlay(type);
    }
}
