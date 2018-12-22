using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private  float ACCELERATION = 1;
    [SerializeField]
    private  float FULL_TURN_ANGLE = 30;
    [SerializeField]
    private float FULL_BRAKING_POWER = 50;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float turnPower = 0;
        float accellerationPower = 0;
        float brakingPower = 0;

        var localVelocity = transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity);

        if (Input.GetKey(KeyCode.W))
        {
            accellerationPower = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(localVelocity.z > 1)
            {
                brakingPower = 1;
            }
            else
            {
                accellerationPower = -0.5f;
            }

        }

        if (Input.GetKey(KeyCode.Space))
        {
            brakingPower = 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            turnPower = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnPower = 1;
        }


        float turnAngle = turnPower * FULL_TURN_ANGLE;
        float torque = ACCELERATION * accellerationPower;

        _rearWheelLeft.motorTorque = torque;
        _rearWheelRight.motorTorque = torque;

        //_frontWheelLeft.transform.rotation = Quaternion.Euler(0, turnAngle, 0);
        //_frontWheelRight.transform.rotation = Quaternion.Euler(0, turnAngle, 0);

        _frontWheelLeft.steerAngle = turnAngle;
        _frontWheelRight.steerAngle = turnAngle;

        _rearWheelLeft.brakeTorque = brakingPower * FULL_BRAKING_POWER * REAR_BRAKE_BALANCE;
        _rearWheelRight.brakeTorque = brakingPower * FULL_BRAKING_POWER * REAR_BRAKE_BALANCE;
        _frontWheelLeft.brakeTorque = brakingPower * FULL_BRAKING_POWER * FRONT_BRAKE_BALANCE;
        _frontWheelRight.brakeTorque = brakingPower * FULL_BRAKING_POWER * FRONT_BRAKE_BALANCE;


        //Apply downforce

        float forwardSpeed = Mathf.Abs(localVelocity.z);

        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -forwardSpeed * 0.1f, 0));
    }
}
