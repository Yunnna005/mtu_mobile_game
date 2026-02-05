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
            }
            Debug.Log("No Object hit");
        }
    }

    internal void DragAt(Ray ray)
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
            //camera.Move();
        }
    }

    internal void Pinch(Vector2 touch1, Vector2 touch2)
    {
        if (selectedObject != null)
        {
            Vector2 new_Scale = touch1/touch2;
            selectedObject.Scale(new_Scale);
        }
        else
        {
            Vector2 trs = touch2 - touch1;
            camera.Zoom(trs);
        }    
    }
}
