using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float searchStopDistance = 1f;
    [SerializeField]
    private GameObject targetPoint;
    private bool hasWrench = false;
    private RaycastHit2D hit;
    private Vector3 newPosition;
    private AILerp lerp;

	void Awake()
    {
        lerp = GetComponent<AILerp>();
	}
	

	void Update ()
    {
		if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero,0f);
                if (hit)
                {
                    if (hit.transform.tag == "Interactable") { hit.transform.GetComponent<IInteractable>().Interact(); }

                }
            }

            if (Vector3.Distance(transform.position, targetPoint.transform.position) < searchStopDistance) { lerp.canSearch = false; }
        }
	}

    public void setMovePoint(Vector3 position, int turnAmount)
    {
        targetPoint.transform.position = position;
        lerp.canSearch = true;
    }
}
