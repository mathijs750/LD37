using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private Vector2 parallaxScalePerisope, parallaxScaleDrill;
    [SerializeField]
    private Vector2 movementLimit;
    private Transform[] layers;
    private Vector3 deltaPos;
    private AudioMomentController momentController;

	void Start ()
    {
        momentController = GetComponent<AudioMomentController>();
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

                for (int i = 1; i < layers.Length; i++)
                {
                    layers[i].position = new Vector3(-deltaPos.x * i * parallaxScaleDrill.x,
                        -deltaPos.y * i * parallaxScaleDrill.y, layers[i].position.z);
                }
            }
            else
            {
                deltaPos.y = Mathf.Clamp(Camera.main.transform.position.y, -movementLimit.y, movementLimit.y);
                for (int i = 1; i < layers.Length; i++)
                {
                    layers[i].position = new Vector3(0, -deltaPos.y * i * parallaxScalePerisope.y, layers[i].position.z);
                }

            } 
        }
	}

    void parallax_OnModeChange(MovementMode old, MovementMode current)
    {
        if (current == MovementMode.Periscope)
        {
            transform.localScale = Vector3.one;
            momentController.ChangeValue(true);
        }
        else if (current == MovementMode.Drill)
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);
            momentController.ChangeValue(false);
        }
    }

    void OnEnable()
    {
        CameraController.OnMovementModeChange += parallax_OnModeChange;
    }

    void OnDisable()
    {
        CameraController.OnMovementModeChange -= parallax_OnModeChange;
    }
}
