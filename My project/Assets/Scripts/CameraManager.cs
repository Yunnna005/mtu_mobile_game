using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject CameraRotationPoint;
    Transform rotationPoint;
    ManagerActionsScript manager;

    private void Start()
    {
        rotationPoint = CameraRotationPoint.transform;
        Input.gyro.enabled = true;
        manager = FindAnyObjectByType<ManagerActionsScript>();
    }
    private void Update()
    {
        if (manager.selectedObject != null && manager != null)
        {
            float rotationSpeed = -Input.gyro.rotationRateUnbiased.z;

            transform.Rotate(0, 0, rotationSpeed * 0.5f);
        }
    }
    public void Move(Vector2 delta)
    {
        transform.Translate(-delta.x * Time.deltaTime, -delta.y * Time.deltaTime, 0f,Space.Self);
    }

    public void Zoom(float new_scale)
    {
        Camera.main.fieldOfView -= new_scale * 0.1f;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 80f);
    }

    internal void RotateAround(float angle)
    {
        
        rotationPoint.transform.Rotate(0, angle * Time.deltaTime, 0 );
    }

    internal void Rotate(float angle)
    {
        transform.Rotate(0, 0, angle * Time.deltaTime*0.3f);
    }
}
