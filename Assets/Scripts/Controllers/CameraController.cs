﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode
{
    Drill,
    Periscope,
    Overlay
}

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float MouseFactor = 1;
    [SerializeField]
    private Transform focalPoint;
    [SerializeField]
    private Vector2 cameraBounds;
    [SerializeField]
    private float periscopeSpeed = .0004f;

    private float cameraZoomLevel = 1;
    private float maxZoomLevel;
    private Vector2 cameraLimits, cameraOffset;
    private Vector3 newPos;
    private new Camera camera;

    private MovementMode _mode = MovementMode.Drill;
    public MovementMode mode { get { return _mode; } }

    void Awake()
    {
        camera = GetComponent<Camera>();
        maxZoomLevel = CalculateMaxZoomLevel();
        cameraLimits = CalculateCameraLimits();
        transform.position = new Vector3(Mathf.Clamp(focalPoint.position.x, -cameraLimits.x, cameraLimits.x),
                Mathf.Clamp(focalPoint.position.y, -cameraLimits.y, cameraLimits.y), -10);
        newPos = transform.position;
        cameraOffset = Vector2.zero;
    }

    void Update()
    {
        if (GameManager.instance.globalState == GlobalState.Gameplay)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                cameraZoomLevel = Mathf.Clamp(cameraZoomLevel -= Input.GetAxis("Mouse ScrollWheel") * 2, 1, maxZoomLevel);
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, CalculateCameraSize(cameraZoomLevel), Time.deltaTime * 2);
                cameraLimits = CalculateCameraLimits();
            }

            // No x boundss
            if (mode == MovementMode.Periscope)
            {
                if ((Input.mousePosition.x < Screen.width * 0.15f))
                {
                    cameraOffset.x -= (Screen.width * 0.15f - Input.mousePosition.x) * periscopeSpeed;
                }
                else if (Input.mousePosition.x > Screen.width * 0.85f)
                {
                    cameraOffset.x += (Input.mousePosition.x - Screen.width * 0.85f) * periscopeSpeed;
                }
                cameraOffset = new Vector2(cameraOffset.x, cameraOffset.y + (Input.GetAxis("Mouse Y") * MouseFactor));
                newPos = new Vector3(cameraOffset.x, Mathf.Clamp(cameraOffset.y, -cameraLimits.y, cameraLimits.y), -10);
            }
            else
            {
                cameraOffset = new Vector2(cameraOffset.x + (Input.GetAxis("Mouse X") * MouseFactor), cameraOffset.y + (Input.GetAxis("Mouse Y") * MouseFactor));
                newPos = new Vector3(Mathf.Clamp(focalPoint.position.x + cameraOffset.x, -cameraLimits.x, cameraLimits.x),
                Mathf.Clamp(focalPoint.position.y + cameraOffset.y, -cameraLimits.y, cameraLimits.y), -10);
            }

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        }
    }

    public void ChangeMode(MovementMode newMode)
    {
        MovementMode old = _mode;
        _mode = newMode;

        if (old == MovementMode.Periscope)
        {
            cameraZoomLevel = 1;
            cameraLimits = CalculateCameraLimits();
            transform.position = new Vector3(Mathf.Clamp(focalPoint.position.x, -cameraLimits.x, cameraLimits.x),
                    Mathf.Clamp(focalPoint.position.y, -cameraLimits.y, cameraLimits.y), -10);
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
        float maxOrhoSize = cameraBounds.x / Camera.main.aspect * .5f;
        Debug.Log(maxOrhoSize / CalculateCameraSize(1));
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
        return new Vector2((cameraBounds.x - camRect.x) / 2, (cameraBounds.y - camRect.y) / 2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(Vector3.zero, cameraBounds);
    }
}
