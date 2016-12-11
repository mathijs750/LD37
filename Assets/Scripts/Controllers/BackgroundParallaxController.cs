using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private float parallaxScaleX = 2, parallaxScaleY = 1;
    [SerializeField]
    private Vector2 movementLimit;
    private Transform[] layers;
    private Vector3 deltaPos;

	void Start ()
    {
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        System.Array.Reverse(layers);
	}

	void Update ()
    {
        if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (cameraController.mode == MovementMode.Drill)
            {
                deltaPos.x = Mathf.Clamp(Camera.main.transform.position.x, -movementLimit.x, movementLimit.x);
                deltaPos.y = Mathf.Clamp(Camera.main.transform.position.y, -movementLimit.y, movementLimit.y);
            }
            else
            {
                deltaPos.x = 0;
            }

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].position = new Vector3(-deltaPos.x * i * parallaxScaleX,
                    -deltaPos.y * i * parallaxScaleY, layers[i].position.z);
            }
        }


	}
}
