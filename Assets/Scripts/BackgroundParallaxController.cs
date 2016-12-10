using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxController : MonoBehaviour
{
    [SerializeField]
    private bool useXoffset = true;
    [SerializeField]
    private float parallaxScaleX = 2, parallaxScaleY = 1;
    [SerializeField]
    private Vector2 movementLimit;
    private Transform[] layers;
    private Vector3 deltaPos, pivot;


	void Start ()
    {
        //transform.position = Camera.main.transform.position + new Vector3(0,0,10);
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        System.Array.Reverse(layers);
	}

	void Update ()
    {
        if (useXoffset)
        {
            deltaPos.x = Mathf.Clamp(Camera.main.transform.position.x - layers[0].position.x, -movementLimit.x, movementLimit.x);
        }
        else
        {
            deltaPos.x = 0;
        }
        deltaPos.y = Mathf.Clamp(Camera.main.transform.position.y - layers[0].position.y, -movementLimit.y, movementLimit.y);
        for (int i = 1; i < layers.Length; i++)
        {
            layers[i].position = new Vector3(-deltaPos.x * i * parallaxScaleX,
                -deltaPos.y * i * parallaxScaleY,  layers[i].position.z);
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(pivot,0.1f);
    }
}
