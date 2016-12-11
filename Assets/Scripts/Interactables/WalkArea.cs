using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkArea : MonoBehaviour, IInteractable
{
    void IInteractable.Interact()
    {

    }

    public float waitSeconds = 0;
    public float speedOut = 0;
}
