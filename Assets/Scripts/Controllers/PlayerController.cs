using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Texture2D walkCursor, interactCursor, defaultCursor;
    [SerializeField]
    private float searchStopDistance = 1f;
    [SerializeField]
    private GameObject targetPoint;
    private RaycastHit2D hit;
    private Vector3 newPosition, lastPosition;
    private AILerp lerp;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject _holdObject;
    public GameObject holdObject { get { return _holdObject; } }

    private bool _hasWrench = false;
    public bool hasWrench {
        get { return _hasWrench; }
        set { _hasWrench = value; }
    }


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
            hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

            if (hit)
            {
                if (hit.transform.tag == "Interactable")
                {
                    if (hit.transform.name == "Walk" || hit.transform.name == "Hang")
                    {
                        Cursor.SetCursor(walkCursor, new Vector2(10 ,10), CursorMode.Auto);
                    }
                    else
                    {
                        Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
                    }
                }
            }
            else
            {
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
            }

            if (Input.GetMouseButtonUp(0))
            {
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
                Vector3 diff = transform.position - targetPoint.transform.position;
                animator.SetFloat("Horizontalspeed", diff.x);
                animator.SetFloat("Verticalspeed", diff.y);
                if (diff.x < 0) { spriteRenderer.flipX = true; }
                else { spriteRenderer.flipX = false; }
            }
            
        }
    }

    public void setMovePoint(Vector3 position, int turnAmount)
    {
        targetPoint.transform.position = position;
        lerp.canSearch = true;
    }
}
