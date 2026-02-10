using UnityEditor;
using UnityEngine;

public class CapsuleManager : MonoBehaviour, IInteractable
{
    float start_scale = 2f;

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

    public void Rotate()
    {
        throw new System.NotImplementedException();
    }

    public void Scale(float new_scale)
    {
        float scaleSpeed = 0.005f;
        float newScale = transform.localScale.x + new_scale * scaleSpeed;
        newScale = Mathf.Clamp(newScale, 0.3f, 5f);

        transform.localScale = Vector3.one * newScale;
    }
}
