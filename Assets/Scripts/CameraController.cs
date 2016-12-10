using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float MouseFactor = 1;
    [SerializeField]
    private Transform focalPoint;
    [SerializeField]
    private Rect cameraBounds;

    private float cameraZoomLevel = 1;
    private float maxZoomLevel;
    private Vector2 cameraLimits;
    private Vector3 cameraWobble;
    private new Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
        maxZoomLevel = CalculateMaxZoomLevel();
        cameraLimits = CalculateCameraLimits();
        transform.position = new Vector3(Mathf.Clamp(focalPoint.position.x, -cameraLimits.x, cameraLimits.x),
                Mathf.Clamp(focalPoint.position.y, -cameraLimits.y, cameraLimits.y), -10);
        cameraWobble = transform.position;
    }

    void Update()
    {
        if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                cameraZoomLevel = Mathf.Clamp(cameraZoomLevel -= Input.GetAxis("Mouse ScrollWheel")*2, .5f, maxZoomLevel);
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, CalculateCameraSize(cameraZoomLevel), Time.deltaTime*2);
                cameraLimits = CalculateCameraLimits();
            }

            cameraWobble = new Vector3(Mathf.Clamp(focalPoint.position.x  + Input.GetAxis("Mouse X")* MouseFactor, - cameraLimits.x, cameraLimits.x),
                Mathf.Clamp(focalPoint.position.y + Input.GetAxis("Mouse Y")* MouseFactor, - cameraLimits.y, cameraLimits.y), -10);

            transform.position = Vector3.Lerp(transform.position, cameraWobble, Time.deltaTime);
        }
    }

    private float CalculateCameraSize(float zoomLevel)
    {
        // (vres / pixelsScale) / 2
        // pixscale = 100
        return (Screen.height * .005f) * zoomLevel;
    }

    private float CalculateMaxZoomLevel()
    {
        // Given that
        //  height = 2 * Camera.main.orthographicSize;
        //  width = height * Camera.main.aspect;
        float maxOrhoSize = cameraBounds.size.x / Camera.main.aspect * .5f;
        return maxOrhoSize / CalculateCameraSize(1);
    }

    private Vector2 CalculateCameraRect()
    {
        // Given that
        //  height = 2 * Camera.main.orthographicSize;
        //  width = height * Camera.main.aspect;
        float height = 2 * Camera.main.orthographicSize;
        return new Vector2(height * Camera.main.aspect, height);
    }

    private Vector2 CalculateCameraLimits()
    {
        Vector2 camRect = CalculateCameraRect();
        // divided by two to calculations
        return new Vector2((cameraBounds.size.x - camRect.x) / 2, (cameraBounds.size.y - camRect.y) / 2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(cameraBounds.center, cameraBounds.size);
    }
}
