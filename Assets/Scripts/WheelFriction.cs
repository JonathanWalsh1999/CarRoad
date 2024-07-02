using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFriction : MonoBehaviour
{
    public Transform[] wheels;
    public float maxTorque = 300.0f;
    public float brakeTorque = 5000.0f;
    public float maxSteerAngle = 30.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float motor = maxTorque * Input.GetAxis("Vertical");
        float steering = maxSteerAngle * Input.GetAxis("Horizontal");

        for (int i = 0; i < wheels.Length; i++)
        {
            if (i < 2) // Assuming front wheels are for steering
            {
                ApplySteering(wheels[i], steering);
            }
            ApplyMotorTorque(wheels[i], motor);
        }
    }

    private void ApplyMotorTorque(Transform wheel, float torque)
    {
        rb.AddForceAtPosition(wheel.forward * torque, wheel.position);
    }

    private void ApplySteering(Transform wheel, float steerAngle)
    {
        wheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
    }
}
