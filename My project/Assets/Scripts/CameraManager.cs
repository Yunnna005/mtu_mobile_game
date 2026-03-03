using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject CameraRotationPoint;
    Transform rotationPoint;

    private void Start()
    {
        rotationPoint = CameraRotationPoint.transform;
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

    internal void CameraRotation(Quaternion quaternion)
    {
        rotationPoint.transform.Rotate(0, quaternion.y * 0.5f, 0);
    }
}
