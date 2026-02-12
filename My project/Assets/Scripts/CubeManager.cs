using UnityEngine;

public class CubeManager : MonoBehaviour, IInteractable
{
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

    public void Scale(float new_scale)
    {
        float scaleSpeed = 0.005f;
        float newScale = transform.localScale.x + new_scale * scaleSpeed;
        newScale = Mathf.Clamp(newScale, 0.3f, 5f);

        transform.localScale = Vector3.one * newScale;
    }

    public void Drag(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = new Vector2(hit.point.x, hit.point.y);
        }
    }

    public void Rotate(float angle)
    {
        transform.Rotate(0, 0, angle * Time.deltaTime);
    }
}
