using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour, ICarController
{
    private CarScript _car;

    public void UpdateControls()
    {
        var localVelocity = transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity);

        if (Input.GetKey(KeyCode.W))
        {
            _car.SetAccelerationPower(1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (localVelocity.z > 1)
            {
                _car.SetBrakingPower(1);
            }
            else
            {
                _car.SetAccelerationPower(-0.5f);

            }

        }

        if (Input.GetKey(KeyCode.Space))
        {
            _car.SetBrakingPower(1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _car.SetTurningPower(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _car.SetTurningPower(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _car = GetComponent<CarScript>();
        _car.RegisterController(this);
    }
}
