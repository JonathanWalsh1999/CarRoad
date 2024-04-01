using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    public WheelCollider wheelL;
    public WheelCollider wheelR;
    public float AntiRoll = 5000.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WheelHit hit = new WheelHit(); //Can see what the wheel collider is hitting etc.
        float travelL = 1.0f;
        float travelR = 1.0f;


        bool groundedL = wheelL.GetGroundHit(out hit);//Check if the wheel hits the ground and then output the hit value from method
        if (groundedL)
        {
            //Get distance below the car
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
        }

        bool groundedR = wheelR.GetGroundHit(out hit);//Check if the wheel hits the ground and then output the hit value from method
        if (groundedR)
        {
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
        }

        //Roll right if moved left and vice versa
        float antiRollForce = (travelL - travelR) * AntiRoll;

        //Add forces to rigid body. Forces only applied when wheels on the ground
        if (groundedL)
        {
            rb.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);
        }

        if (groundedR)
        {
            rb.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
        }
    }
}
