using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputCaptureScript : MonoBehaviour
{
    private float timer;
    private bool hasMoved;
    private float tapTreshold = 0.5f;

    ManagerActionsScript theManager;

    void Start()
    {
        theManager = FindObjectOfType<ManagerActionsScript>();

    }

    void Update()
    {
        Touch t, t2;
        if (Input.touchCount > 0)
        {
            t = Input.touches[0];
            switch (t.phase)
            {
                case TouchPhase.Began:
                    timer = 0f;
                    hasMoved = false;

                    break;

                case TouchPhase.Stationary:
                    timer += Time.deltaTime;
                    break;

                case TouchPhase.Moved:
                    hasMoved = true;
                    timer += Time.deltaTime;
                    Ray ray = Camera.main.ScreenPointToRay(t.position);
                    Vector3 delta = t.deltaPosition;

                    theManager.DragAt(ray, delta);

                    break;

                case TouchPhase.Ended:
                    if ((timer < tapTreshold) && !hasMoved)
                    {
                        theManager.TapAt(t.position);
                    }
                    
                    break;
            }
        }

        if (Input.touchCount >= 2)
        {
            t = Input.GetTouch(0);
            t2 = Input.GetTouch(1);

            theManager.Pinch(t, t2);
            theManager.isTwoTouches = true;

            theManager.GetRotation(theManager.GetAngle(t, t2));
        }

        if (Input.touchCount >= 3)
        {
            t = Input.GetTouch(0);
            t2 = Input.GetTouch(1);
            theManager.isTwoTouches = false;
            theManager.isRotatingAround = true;
            theManager.GetRotation(theManager.GetAngle(t, t2));
        }
    }
}
