using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pickUpType
{
    part,
    wrench
}

public class PickUp : MonoBehaviour, IInteractable
{
    [SerializeField]
    private pickUpType type;
    [SerializeField]
    private PlayerController player;
    private DrillManager drillManager;
    private bool pickedUp = false;

    void Awake()
    {
        drillManager = transform.parent.GetComponent<DrillManager>();
    }

    public void Interact()
    {
        if (pickedUp) { return; }
        if (Vector3.Distance(player.transform.position, transform.position) > 2f)
        {
            if (type == pickUpType.wrench)
            {
                transform.SetParent(player.transform.GetChild(0).GetChild(0));
                transform.localPosition = Vector3.zero;
                player.hasWrench = true;
                pickedUp = true;
            }
        }
    }
}
