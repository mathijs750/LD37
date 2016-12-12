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
    private Vector3 newPosition, lastPosition;
    private AILerp lerp;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject _holdObject;
    public GameObject holdObject { get { return _holdObject; } }


    void Awake()
    {
        lerp = GetComponent<AILerp>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }


    void Update()
    {
        if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
                if (hit)
                {
                    if (hit.transform.tag == "Interactable") { hit.transform.GetComponent<IInteractable>().Interact(); }

                }
            }

            if (Vector3.Distance(transform.position, targetPoint.transform.position) < searchStopDistance) { lerp.canSearch = false; }


            hit = Physics2D.Raycast(transform.position, Vector2.zero, 1f);
            if (hit)
            {
                if (hit.transform.name == "Walk") { animator.SetBool("isHanging", false); }
                else { animator.SetBool("isHanging", true); }
            }
            Vector3 diff = transform.position - targetPoint.transform.position;

            Debug.Log(Mathf.Abs(diff.x * 10f));
            animator.SetFloat("Horizontalspeed", diff.x);
            animator.SetFloat("Verticalspeed", diff.y);
            if (diff.x < 0) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
        }
    }

    public void setMovePoint(Vector3 position, int turnAmount)
    {
        targetPoint.transform.position = position;
        lerp.canSearch = true;
    }
}
