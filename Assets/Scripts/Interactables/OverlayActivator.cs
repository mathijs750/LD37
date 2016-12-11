using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayActivator : MonoBehaviour, IInteractable
{
    [SerializeField]
    private OverlayType type;
    private DrillManager drillManager;

    void Start()
    {
        drillManager = transform.parent.GetComponent<DrillManager>();
    }

    public void Interact()
    {
        if (type == OverlayType.DigDeeper)
        {
            Debug.Log("DigDeeper doen!");
            if (drillManager.allMachinesFixed())
            {
                GameManager.instance.nextLayer();
                return;
            }
        }
        else
        {
            GameManager.instance.openOverlay(type);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawCube(Vector3.zero, GetComponent<BoxCollider2D>().size);
    }
}
