using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayActivator : MonoBehaviour, IInteractable
{
    [SerializeField]
    private OverlayType type;
    [SerializeField]
    private DrillManager drillManager;

    public void Interact()
    {
        if (type == OverlayType.DigDeeper)
        {
            if (drillManager.allMachinesFixed())
            {
                GameManager.instance.openOverlay(OverlayType.DigDeeper);
                return;
            }
        }

        GameManager.instance.openOverlay(type);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawCube(Vector3.zero, GetComponent<BoxCollider2D>().size);
    }
}
