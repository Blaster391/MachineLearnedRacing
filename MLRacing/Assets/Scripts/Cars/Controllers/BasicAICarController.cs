using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAICarController : MonoBehaviour, ICarController
{
    [SerializeField]
    private SplineComponent _racingLine;

    private CarScript _car;

    public void UpdateControls()
    {
        var currentPosition = transform.position + transform.forward;
        var closestPointOnRacingLine = _racingLine.FindClosest(currentPosition);
 

        var closestPointLocal = transform.InverseTransformDirection(closestPointOnRacingLine);
        var variance = (currentPosition - closestPointOnRacingLine);

        Debug.Log("Pos: " + currentPosition);
        Debug.Log("Closest: " + closestPointOnRacingLine);

        Debug.Log("Local Pos: " + transform.localPosition);
        Debug.Log("Closest Local: " + closestPointLocal);

        Debug.Log("Variance: " + variance);

        var varianceMag = variance.magnitude;

        if(variance.x > 0)
        {
            _car.SetTurningPower(varianceMag);
        }
        else
        {
            _car.SetTurningPower(-varianceMag);
        }

        _car.SetAccelerationPower(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        _car = GetComponent<CarScript>();
        _car.RegisterController(this);
    }
}
