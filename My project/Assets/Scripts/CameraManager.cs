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
    public void Move(Vector2 delta)
    {
        transform.Translate(-delta.x * Time.deltaTime, -delta.y * Time.deltaTime, 0f,Space.World);
    }

    public void Zoom(float new_scale)
    {
        Camera.main.fieldOfView -= new_scale * 0.1f;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 80f);
    }
}
