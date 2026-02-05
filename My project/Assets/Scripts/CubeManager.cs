using UnityEngine;

public class CubeManager : MonoBehaviour, IInteractable
{
    float start_scale = 2f;

    Renderer renderer;
    Color defaultColor = Color.blue;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void YouHaveBeenSelected()
    {
        Debug.Log("Tapped on object: " + this.name);
        Debug.Log("Hit texture coordinates: " + this.transform.position.ToString());
        renderer.material.color = Color.lightSkyBlue;
    }

    public void YouHaveBeenUnselected()
    {
        renderer.material.color = defaultColor;
    }

    public void Move(Vector2 newPosition)
    {
        transform.position = newPosition;
        //add to plane
    }

    public void Rotate()
    {
        throw new System.NotImplementedException();
    }

    public void Scale(Vector2 new_scale)
    {
        transform.localScale = new_scale * start_scale;
    }
}
