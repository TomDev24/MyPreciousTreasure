using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    // Величина смещения. -1.0 = максимально влево,
    // +1.0 = максимально вправо
    private float _sidewaysMotion = 0.0f;
    public bool accelControl = false;
    public bool touchControl = true;

    public float sidewaysMotion
    {
        get
        {
            return _sidewaysMotion;
        }
    }

    private void Update()
    {
        if (accelControl)
        {
            Vector3 accel = Input.acceleration;
            _sidewaysMotion = accel.x;
        }
        if (touchControl)
        {
            if (Input.touchCount > 0)
            {
                Vector3 touchPosition = Input.GetTouch(0).position;
                if (touchPosition.x > Screen.width * 0.5f)
                    _sidewaysMotion = 1;
                else
                    _sidewaysMotion = -1;
            } else
            {
                _sidewaysMotion = 0;
            }

        }

        float horMov = Input.GetAxis("Horizontal");
        if (horMov != 0)
        {
            _sidewaysMotion = horMov;
        }
       
    }
}
