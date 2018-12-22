using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{

    [SerializeField]
    private float ACCELERATION = 100;
    [SerializeField]
    private float FULL_TURN_ANGLE = 45;
    [SerializeField]
    private float FULL_BRAKING_POWER = 200;
    [SerializeField]
    private float FRONT_BRAKE_BALANCE = 1;
    [SerializeField]
    private float REAR_BRAKE_BALANCE = 1;

    [SerializeField]
    private WheelCollider _frontWheelLeft;
    [SerializeField]
    private WheelCollider _frontWheelRight;
    [SerializeField]
    private WheelCollider _rearWheelLeft;
    [SerializeField]
    private WheelCollider _rearWheelRight;

    private ICarController _controller = null;

    private float _turningPower = 0;
    private float _accelerationPower = 0;
    private float _brakingPower = 0;

    public void RegisterController(ICarController c)
    {
        _controller = c;
    }

    // Update is called once per frame
    void Update()
    {
        ClearControls();

        if (_controller != null)
        {
            _controller.UpdateControls();
        }
        ApplyControls();
    }
    private void ClearControls()
    {
        _turningPower = 0;
        _accelerationPower = 0;
        _brakingPower = 0;
    }


    private void ApplyControls()
    {
        var localVelocity = transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity);

        float turnAngle = _turningPower * FULL_TURN_ANGLE;
        float torque = _accelerationPower * ACCELERATION;

        _rearWheelLeft.motorTorque = torque;
        _rearWheelRight.motorTorque = torque;

        _frontWheelLeft.steerAngle = turnAngle;
        _frontWheelRight.steerAngle = turnAngle;

        _rearWheelLeft.brakeTorque = _brakingPower * FULL_BRAKING_POWER * REAR_BRAKE_BALANCE;
        _rearWheelRight.brakeTorque = _brakingPower * FULL_BRAKING_POWER * REAR_BRAKE_BALANCE;
        _frontWheelLeft.brakeTorque = _brakingPower * FULL_BRAKING_POWER * FRONT_BRAKE_BALANCE;
        _frontWheelRight.brakeTorque = _brakingPower * FULL_BRAKING_POWER * FRONT_BRAKE_BALANCE;


        //Apply downforce
        float forwardSpeed = Mathf.Abs(localVelocity.z);
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -forwardSpeed * 0.1f, 0));
    }

    public void SetTurningPower(float f)
    {
        _turningPower = Mathf.Clamp(f, -1, 1);
    }

    public void SetBrakingPower(float f)
    {
        _brakingPower = Mathf.Clamp01(f);
    }

    public void SetAccelerationPower(float f)
    {
        _accelerationPower = Mathf.Clamp(f,-1,1);
    }
}
