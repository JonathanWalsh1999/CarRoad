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

    public float brakeStrength;
    public List<GameObject> tailLights;

    public UIManager uim;

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

        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brake ? new Color(1.0f, 0.111f, 0.111f) : Color.black);
            if (im.brake)
            {
                Debug.Log("hello");
            }
        }

        uim.ChangeText(transform.InverseTransformVector(rb.velocity).z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WheelCollider wheelCollider in throttleWheels)
        {
            if(im.brake)
            {
                wheelCollider.motorTorque = 0.0f;
                wheelCollider.brakeTorque = brakeStrength * Time.deltaTime * 100000000000000000000.0f;
            }
            else
            {
                wheelCollider.motorTorque = strengthCoefficent * Time.deltaTime * im.throttle;
                wheelCollider.brakeTorque = 0.0f;
            }
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
