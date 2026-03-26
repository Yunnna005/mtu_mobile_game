using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    CameraManager camera_manager;
    private void Start()
    {
        camera_manager = FindAnyObjectByType<CameraManager>();
    }
    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TurnOnOffCameraMovement()
    {
        int click = 0;
        if (click == 0)
        {
            camera_manager.isMoving = true;
            this.GetComponentInChildren<Text>().text = "Camera Movement ON";
            click = 1;
        }
        else
        {
            camera_manager.isMoving = false;
            this.GetComponentInChildren<Text>().text = "Camera Movement OFF";
            click = 0;
        }
    }

    public void TurnOnOffCameraRotationItself()
    {
        int click = 0;
        if (click == 0)
        {
            camera_manager.isRotatingAroundItself = true;
            this.GetComponentInChildren<Text>().text = "Camera Rotation Itself ON";
            click = 1;
        }
        else
        {
            camera_manager.isRotatingAroundItself = false;
            this.GetComponentInChildren<Text>().text = "Camera Rotation Itself OFF";
            click = 0;
        }
    }

    public void TurmOnOffGyro()
    {
        int click = 0;
        if (click == 0)
        {
            camera_manager.isGyro = true;
            this.GetComponentInChildren<Text>().text = "Gyro ON";
            click = 1;
        }
        else
        {
            camera_manager.isGyro = false;
            this.GetComponentInChildren<Text>().text = "Gyro OFF";
            click = 0;
        }
    }

    public void TurnOnOffCameraZoom()
    {
        int click = 0;
        if (click == 0)
        {
            camera_manager.isZoom = true;
            this.GetComponentInChildren<Text>().text = "Camera Zoom ON";
            click = 1;
        }
        else
        {
            camera_manager.isZoom = false;
            this.GetComponentInChildren<Text>().text = "Camera Zoom OFF";
            click = 0;
        }
    }
}
