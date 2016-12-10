using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasWrench = false;
    private RaycastHit2D hit;

	void Awake()
    {
		
	}
	

	void Update ()
    {
		if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero,0f);
                if (hit.transform.tag == "Interactable") { hit.transform.GetComponent<IInteractable>().Interact(); }
            }
        }
	}
}
