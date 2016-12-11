using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType {
    pickUp,
    pickUpReciever,
    overlayActivator
}


public class DrillManager : MonoBehaviour
{
    private Dictionary<int, InteractionType> interactables;
	void Awake () {
        interactables = new Dictionary<int, InteractionType> { { 0, InteractionType.overlayActivator } };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact(int childIndex)
    {
    }
}
