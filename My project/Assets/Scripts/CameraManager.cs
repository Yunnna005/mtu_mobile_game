using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject CameraRotationPoint;
    Transform rotationPoint;
    ManagerActionsScript manager;
    public bool isRotatingAroundItself = false, isGyro = false, isZoom = false, isMoving = false;

    private void Start()
    {
        rotationPoint = CameraRotationPoint.transform;
        Input.gyro.enabled = true;
        manager = FindAnyObjectByType<ManagerActionsScript>();
    }
    private void Update()
    {
        if (manager.selectedObject != null && manager != null && isGyro)
        {
            float rotationSpeed = -Input.gyro.rotationRateUnbiased.z;

            transform.Rotate(0, 0, rotationSpeed * 0.5f);
        }
    }
    public void Move(Vector2 delta)
    {
        if (isMoving) 
        {
            transform.Translate(-delta.x * Time.deltaTime, -delta.y * Time.deltaTime, 0f, Space.Self);
        }
    }

    public void Zoom(float new_scale)
    {
        if (isZoom)
        {
            Camera.main.fieldOfView -= new_scale * 0.1f;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 80f);
        }
    }

    internal void RotateAround(float angle)
    {
        transform.RotateAround(rotationPoint.position, Vector3.up, angle * 0.1f);
    }

    internal void Rotate(float angle)
    {
        if (isRotatingAroundItself)
        {
            float smoothAngle = Mathf.Lerp(0, angle, 0.5f);
            transform.Rotate(0, 0, smoothAngle);
        }
    }
}
