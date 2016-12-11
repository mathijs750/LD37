using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasWrench = false;
    private RaycastHit2D hit;
    private Vector3 newPosition;

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

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime *3);
            if (Vector3.Distance(newPosition, transform.position) < 0.5f) { transform.position = newPosition; }
        }
	}

    public void setMovePoint(Vector3 position, int turnAmount)
    {
        newPosition = position;
    }
}
