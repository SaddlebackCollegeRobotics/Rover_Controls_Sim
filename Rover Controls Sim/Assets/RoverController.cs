using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider backLeft;
    [SerializeField] private WheelCollider backRight;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;

    private float horizontal = 0;
    private float vertical = 0;

    private float currentBrakeForce = 0;
    private bool isBreaking = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isBreaking = Input.GetButton("Fire1") || vertical == 0;

        currentBrakeForce = isBreaking ? brakeForce : 0;

        frontLeft.motorTorque = vertical * motorForce;
        frontRight.motorTorque = vertical * motorForce;
        backLeft.motorTorque = vertical * motorForce;
        backRight.motorTorque = vertical * motorForce;

        frontLeft.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        frontRight.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;

    }
}
