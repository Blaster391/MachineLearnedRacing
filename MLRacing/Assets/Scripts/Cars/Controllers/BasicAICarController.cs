using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAICarController : MonoBehaviour, ICarController
{
    [SerializeField]
    private SplineComponent _racingLine;

    [SerializeField]
    private float _lookAheadDistance = 1;

    private CarScript _car;



    public void UpdateControls()
    {
        var currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

        var pointToCheck = transform.position + currentVelocity * _lookAheadDistance;
        var closestPointOnRacingLine = _racingLine.FindClosest(pointToCheck);
 
        var heading = (pointToCheck - closestPointOnRacingLine);
        var varianceMag = heading.magnitude;


        //Determine if car is to the left or right of racing line
        Vector3 perp = Vector3.Cross(transform.forward, heading);
        float dir = Vector3.Dot(perp, transform.up);

        Debug.Log(dir);

        if (dir < 0)
        {
            _car.SetTurningPower(varianceMag);
        }
        else
        {
            _car.SetTurningPower(-varianceMag);
        }


        _car.SetAccelerationPower(1);


        _debugPointToCheck = pointToCheck;
        _debugClosestPoint = closestPointOnRacingLine;
    }

    // Start is called before the first frame update
    void Start()
    {
        _car = GetComponent<CarScript>();
        _car.RegisterController(this);
    }


    private Vector3 _debugPointToCheck = new Vector3();
    private Vector3 _debugClosestPoint = new Vector3();
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_debugPointToCheck, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_debugClosestPoint, 0.1f);
    }
}
