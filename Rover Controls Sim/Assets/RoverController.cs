using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoverController : MonoBehaviour
{
    [SerializeField] private bool useROSInput = false; 

    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider backLeft;
    [SerializeField] private WheelCollider backRight;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;

    private PlayerInput playerInput;

    private float currentBrakeForce;
    private bool isBreaking = false;

    private float leftForce;
    private float rightForce;

    private InputAction leftStickAction;
    private InputAction rightStickAction;
    private InputAction rightTriggerAction;

    private float rtAxis;

    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        leftStickAction = playerInput.actions.FindAction("Left Stick");
        rightStickAction = playerInput.actions.FindAction("Right Stick");
        rightTriggerAction = playerInput.actions.FindAction("Right Trigger");
    }

    // Update is called once per frame
    void Update()
    {
        if (useROSInput)
        {
            leftForce = DataListener.instance.wheel_velocities[0];
            rightForce = DataListener.instance.wheel_velocities[1];
        }
        else
        {
            rtAxis = rightTriggerAction.ReadValue<float>();

            if (rtAxis > 0)
            {
                leftForce = rtAxis;
                rightForce = rtAxis;
            }
            else
            {
                leftForce = leftStickAction.ReadValue<Vector2>().y;
                rightForce = rightStickAction.ReadValue<Vector2>().y;
            }
        }

        frontLeft.motorTorque = leftForce * motorForce;
        backLeft.motorTorque = leftForce * motorForce;
        
        frontRight.motorTorque = rightForce * motorForce;
        backRight.motorTorque = rightForce * motorForce;

        isBreaking = (leftForce == 0 && rightForce == 0);
        currentBrakeForce = isBreaking ? brakeForce : 0;

        frontLeft.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;

        frontRight.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;


    }
}
