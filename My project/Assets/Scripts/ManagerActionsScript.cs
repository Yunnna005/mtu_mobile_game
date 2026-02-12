using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ManagerActionsScript : MonoBehaviour
{
    IInteractable selectedObject;
    RaycastHit hit;
    CameraManager camera;

    private void Start()
    {
        camera = FindAnyObjectByType<CameraManager>();
    }
    internal void TapAt(Vector2 position)
    {
        Ray raycast = Camera.main.ScreenPointToRay(position);
        Debug.DrawLine(raycast.origin, raycast.origin + 100 * raycast.direction, Color.red, 2f);

        if (Physics.Raycast(raycast, out hit))
        {
            IInteractable newObject = hit.collider.gameObject.GetComponent<IInteractable>();
            if (newObject != null)
            {
                if (selectedObject != null) {
                    selectedObject.YouHaveBeenUnselected();
                    selectedObject = null;
                }

                selectedObject = newObject;
                selectedObject.YouHaveBeenSelected();
            }
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.YouHaveBeenUnselected();
                selectedObject = null;
            }
            Debug.Log("No Object hit");
        }
    }

    internal void DragAt(Ray ray, Vector2 delta)
    {
        if (selectedObject != null) 
        {
            selectedObject.Drag(ray);
        }
        else
        {
            //camera.Move(delta);
        }
    }

    internal void Pinch(Touch touch1, Touch touch2)
    {
        Vector2 t1Prev = touch1.position - touch1.deltaPosition;
        Vector2 t2Prev = touch2.position - touch2.deltaPosition;

        float prevDistance = Vector2.Distance(t1Prev, t2Prev);
        float currentDistance = Vector2.Distance(touch1.position, touch2.position);

        float trs = currentDistance - prevDistance;

        if (selectedObject != null)
        {
            selectedObject.Scale(trs);
        }
        else
        {
            //camera.Zoom(trs);
        }    
    }

    internal void GetAngle(Touch touch1, Touch touch2)
    {

        float y = touch1.position.y - touch2.position.y; 
        float x = touch1.position.x - touch2.position.x;

        float angle = Mathf.Atan2(y,x) * Mathf.Rad2Deg;
        print(angle);

        if (selectedObject != null) 
        {
            selectedObject.Rotate(angle);
        }
        else
        {
            camera.Rotate(angle);
        }
    }
}
