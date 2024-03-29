using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputManager im;

    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;

    public float strengthCoefficent = 200000.0f;
    public float maxTurn = 20.0f;

    public Transform cm;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if(cm)
        {
            rb.centerOfMass = cm.localPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WheelCollider wheelCollider in throttleWheels)
        {
            wheelCollider.motorTorque = strengthCoefficent * Time.deltaTime * im.throttle;
        }

        foreach(WheelCollider wheelCollider in steeringWheels)
        {
            wheelCollider.steerAngle = maxTurn * im.steer;
        }
    }
}
