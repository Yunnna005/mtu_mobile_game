using UnityEditor;
using UnityEngine;

public class CapsuleManager : MonoBehaviour, IInteractable
{
    Renderer renderer;
    Color defaultColor = Color.red;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void YouHaveBeenSelected() 
    {
        Debug.Log("Tapped on object: " + this.name);
        Debug.Log("Hit texture coordinates: " + this.transform.position.ToString());
        renderer.material.color = Color.lightGoldenRod;
    }

    public void YouHaveBeenUnselected()
    {
        renderer.material.color = defaultColor;
    }

    public void Move(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
