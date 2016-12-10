using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasWrench = false;


	void Awake()
    {
		
	}
	

	void Update ()
    {
		if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                
            }
        }
	}
}
