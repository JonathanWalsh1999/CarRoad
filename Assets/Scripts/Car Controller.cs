using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class CarController : MonoBehaviour
{
    public InputManager im;

    public LightingManager lm;

    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;

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

    private void Update()
    {
        if (im.l)
        {
            lm.ToggleHeadlights();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WheelCollider wheelCollider in throttleWheels)
        {
            wheelCollider.motorTorque = strengthCoefficent * Time.deltaTime * im.throttle;
        }

        foreach(GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0.0f, im.steer * maxTurn, 0.0f);
        }

        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1.0f : -1.0f) 
                / (2 * Mathf.PI * 0.34f), 0.0f, 0.0f);
        }
    }
}
