using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public bool accelerateForwardTrue = false;
    public bool accelerateBackwardTrue = false;
    public bool leftbtnTrue = false;
    public bool rightbtnTrue = false;
    public bool brakeTrue = false;  
    Car CarScript;

    private void Start()
    {
        CarScript = FindObjectOfType<Car>();
    }
    public void PointerDownLeft()
    {
        leftbtnTrue = true;
    }
    public void PointerUpLeft()
    {
        leftbtnTrue = false;
    }
    public void PointerDownRight()
    {
        rightbtnTrue = true;
    }
    public void PointerUpRight()
    {
        rightbtnTrue = false;
    }
    public void PointerUpForward()
    {
        accelerateForwardTrue = false;
    }
    public void PointerDownForward()
    {
        accelerateForwardTrue = true;
    }
    public void PointerDownBackward()
    {
        accelerateBackwardTrue = true;
    }
    public void PointerUpBackward()
    {
        accelerateBackwardTrue = false;
    }
    public void PointerDownBrake()
    {
        brakeTrue = true;
    }
    public void PointerUpBrake()
    {
        brakeTrue = false;
    }


   
    void Update()
    {
        CarMovements();
    }

    void CarMovements()
    {
        if (accelerateForwardTrue)
        {
            CarScript.GoForward();
        }
        if (accelerateBackwardTrue)
        {
            CarScript.GoReverse();
        }
        if (brakeTrue)
        {
            CarScript.HandBrake();
        }
        if (leftbtnTrue)
        {
            CarScript.TurnLeft();
        }
        if (rightbtnTrue)
        {
            CarScript.TurnRight();
        }
        if (!accelerateForwardTrue && !accelerateBackwardTrue )
        {
            CarScript.DecelerateCar();
        }
        if(!leftbtnTrue && !rightbtnTrue )
        {
            CarScript.ResetSteeringAngle();
        }
    }
}
