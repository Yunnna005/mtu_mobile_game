using System;
using UnityEngine;

public class ManagerActionsScript : MonoBehaviour
{
    //3 objects on screan, manager deside what to do with them.
    //Start FindObjectOfType
    //Have Tab(Vector2 position)
    // Ray raycast = Camera.main.ScreenPointToRay(position);
    // Debug.DrawLine(raycast.origin, raycast.oriin +100 + raycast.oriin.dirTouch)
    internal void TapAt(Vector2 position)
    {
        Ray raycast = Camera.main.ScreenPointToRay(position);
        Debug.DrawLine(raycast.origin, raycast.origin + 100 * raycast.direction, Color.red, 2f);
    }
}
