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
                   
                    break;
                case TouchPhase.Ended:
                    if ((timer < tapTreshold) && !hasMoved)
                    {
                        theManager.TapAt(t.position);
                    }
                    else
                    {

                    }
                    
                    break;
            }
        }
    }
}
