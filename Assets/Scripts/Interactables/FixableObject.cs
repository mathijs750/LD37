using UnityEngine;


public enum FixableMode
{
    NewPart,
    Hit
}
public class FixableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Sprite fixedSprite;
    [SerializeField]
    private FixableMode mode;
    [SerializeField]
    private GameObject desiredPart;
    [SerializeField]
    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private DrillManager drillManager;

    void Awake()
    {
        drillManager = transform.parent.GetComponent<DrillManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 2f)
        {
            if (mode == FixableMode.NewPart)
            {
                if (player.holdObject == desiredPart)
                {
                    // positive feedback
                    drillManager.SetFixed(gameObject);
                    spriteRenderer.sprite = fixedSprite;
                }
                else
                {
                    // negative feedback
                }
            }
            else
            {
                // positive feedback
                if (player.hasWrench)
                {
                    drillManager.SetFixed(gameObject);
                    spriteRenderer.sprite = fixedSprite;
                }
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 1, 0, 0.6f);
        Gizmos.DrawCube(Vector3.zero, GetComponent<BoxCollider2D>().size);
    }
}
