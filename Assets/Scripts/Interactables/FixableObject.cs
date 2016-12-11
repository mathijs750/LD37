using UnityEngine;


public enum FixableMode
{
    NewPart,
    Hit
}
public class FixableObject : MonoBehaviour, IInteractable{
    [SerializeField]
    private FixableMode mode;
    [SerializeField]
    private GameObject desiredPart;
    [SerializeField]
    private PlayerController player;
    private DrillManager drillManager;

    void Awake()
    {
        drillManager = transform.parent.GetComponent<DrillManager>();
    }

    public void Interact()
    {
        if (mode == FixableMode.NewPart)
        {
            if (player.holdObject == desiredPart)
            {
                // positive feedback
                drillManager.SetFixed(gameObject);
            }
            else
            {
                // negative feedback
            }
        }
        else
        {
            // positive feedback
            drillManager.SetFixed(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 1, 0, 0.6f);
        Gizmos.DrawCube(Vector3.zero, GetComponent<BoxCollider2D>().size);
    }
}
