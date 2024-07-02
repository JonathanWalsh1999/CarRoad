using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Transform[] springs;

    public float springForce = 35000.0f;
    public float damperForce = 4500.0f;
    public float suspensionTravel = 0.3f;
    public float wheelRadius = 0.35f;
    public LayerMask groundLayer;

    //hmm
    public float maxForce;
    public float maxDistance;
    
    private Rigidbody rb;
    private RaycastHit hit;
    private float[] springCompression;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        springCompression = new float[springs.Length];
    }

   
    void FixedUpdate()
    {
        for(int i = 0; i < springs.Length; i++)
        {
            ApplySuspension(springs[i], i);
        }
    }

    private void ApplySuspension(Transform spring, int index)
    {
        Vector3 springPos = spring.position;
        Vector3 down = -transform.up;

        if (Physics.Raycast(springPos, down, out hit, wheelRadius + suspensionTravel, groundLayer))//max distance equals spring equilibrium
        {
            float compression = 1.0f - (hit.distance - wheelRadius) / suspensionTravel;
            springCompression[index] = Mathf.Clamp(compression, 0.0f, 1.0f);

            Vector3 suspensionForce = transform.up * springForce * springCompression[index];
            Vector3 damperForceVector = transform.up * damperForce * (springCompression[index] - springCompression[index]);

            rb.AddForceAtPosition(suspensionForce + damperForceVector, springPos);

            // Calculate new position for wheel based on suspension compression
            Vector3 newPosition = hit.point + transform.up * wheelRadius;
            spring.position = newPosition;
        }
        else
        {
            springCompression[index] = 0.0f;
        }

        //UpdateWheelPos(spring, hit.point);
    }

    //private void UpdateWheelPos(Transform wheel, Vector3 groundHitPoint)
    //{
    //    wheel.position = groundHitPoint + transform.up * wheelRadius;
    //}
}
