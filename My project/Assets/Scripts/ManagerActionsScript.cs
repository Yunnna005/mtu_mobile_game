using System;
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
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<IInteractable>() == selectedObject)
                {
                    selectedObject.Move(hit.point);
                }
            }
        }
        else
        {
            camera.Move(delta);
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
            camera.Zoom(trs);
        }    
    }
}
