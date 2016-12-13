using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WalkArea : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int turn = 0;

    void IInteractable.Interact()
    {
        player.setMovePoint(Camera.main.ScreenToWorldPoint( Input.mousePosition)+ new Vector3(0,0,10), turn);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0, 0, 1, 0.6f);
        Gizmos.DrawCube(Vector3.zero, GetComponent<BoxCollider2D>().size);
    }
}
