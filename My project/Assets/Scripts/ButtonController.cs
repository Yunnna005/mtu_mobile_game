using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    CameraManager camera_manager;

    // Persistent state fields
    private bool cameraMovementOn = false;
    private bool cameraRotationOn = false;
    private bool gyroOn = false;
    private bool cameraZoomOn = false;

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
        cameraMovementOn = !cameraMovementOn;
        camera_manager.isMoving = cameraMovementOn;
        GetComponentInChildren<Text>().text = cameraMovementOn ? "Camera Movement ON" : "Camera Movement OFF";
    }

    public void TurnOnOffCameraRotationItself()
    {
        cameraRotationOn = !cameraRotationOn;
        camera_manager.isRotatingAroundItself = cameraRotationOn;
        GetComponentInChildren<Text>().text = cameraRotationOn ? "Camera Rotation Itself ON" : "Camera Rotation Itself OFF";
    }

    public void TurmOnOffGyro()
    {
        gyroOn = !gyroOn;
        camera_manager.isGyro = gyroOn;
        GetComponentInChildren<Text>().text = gyroOn ? "Gyro ON" : "Gyro OFF";
    }

    public void TurnOnOffCameraZoom()
    {
        cameraZoomOn = !cameraZoomOn;
        camera_manager.isZoom = cameraZoomOn;
        GetComponentInChildren<Text>().text = cameraZoomOn ? "Camera Zoom ON" : "Camera Zoom OFF";
    }
}