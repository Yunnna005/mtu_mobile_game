using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector2 position_start;
    float screen_width, screen_height, diagonal ;

    private void Start()
    {
        position_start = transform.position;
        screen_width = Screen.width;
        screen_height = Screen.height;
        diagonal = Mathf.Sqrt(screen_width*screen_width + screen_height*screen_height);
    }
    public void Move(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void Zoom(Vector2 new_scale)
    {
        Vector2 trs = new_scale / diagonal;
        transform.position = position_start + trs * transform.position;
    }
}
