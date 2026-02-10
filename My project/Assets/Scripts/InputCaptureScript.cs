using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
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
                    Vector2 delta = t.deltaPosition;

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

            print("In inputCapture");
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            theManager.Pinch(t1, t2);
        }
    }
}
