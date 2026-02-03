using System;
using UnityEditor;
using UnityEngine;

public class ManagerActionsScript : MonoBehaviour
{
    IInteractable selectedObject;
    GameObject objectSelected;
    RaycastHit hit;
    private void Update()
    {
        
    }
    internal void TapAt(Vector2 position)
    {
        Ray raycast = Camera.main.ScreenPointToRay(position);
        Debug.DrawLine(raycast.origin, raycast.origin + 100 * raycast.direction, Color.red, 2f);

        if (Physics.Raycast(raycast, out hit))
        {
            IInteractable newObject = hit.collider.gameObject.GetComponent<IInteractable>();
            objectSelected = hit.collider.gameObject;
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
    }
}
