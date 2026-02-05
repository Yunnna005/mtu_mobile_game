using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InputCaptureScript : MonoBehaviour
{
    private float timer;
    private bool hasMoved;
    private float tapTreshold = 0.5f;
    Vector2 t1, t2;

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

                    t1 = t.position;
                    break;

                case TouchPhase.Stationary:
                    timer += Time.deltaTime;
                    break;

                case TouchPhase.Moved:
                    hasMoved = true;
                    timer += Time.deltaTime;
                    Ray ray = Camera.main.ScreenPointToRay(t.position);
                    theManager.DragAt(ray);

                    while(t.tapCount == 2)
                    {
                        t2 = t.position;
                        theManager.Pinch(t1, t2);
                    }
                    break;

                case TouchPhase.Ended:
                    if ((timer < tapTreshold) && !hasMoved)
                    {
                        theManager.TapAt(t.position);
                    }
                    
                    break;
            }
            Debug.Log(Input.touchCount);
        }
    }
}
