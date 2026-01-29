using System;
using UnityEditor;
using UnityEngine;

public class ManagerActionsScript : MonoBehaviour
{

    internal void TapAt(Vector2 position)
    {
        Ray raycast = Camera.main.ScreenPointToRay(position);
        Debug.DrawLine(raycast.origin, raycast.origin + 100 * raycast.direction, Color.red, 2f);
        RaycastHit hit;

        if (Physics.Raycast(raycast, out hit))
        {
            string objectName = hit.collider.gameObject.name;
            Vector3 hitPoint = hit.collider.gameObject.transform.position;
            Debug.Log("Tapped on object: " + objectName);
            Debug.Log("Hit texture coordinates: " + hitPoint.ToString());
        }
        else
        {
            Debug.Log("No Object hit");
        }
    }

    internal void Move()
    {
        //move an object
    }
}
