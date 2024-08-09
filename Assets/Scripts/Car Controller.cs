using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lesson1Controller;

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

    public float strengthCoefficent = 5000.0f;
    public float maxTurn = 30.0f;

    public Transform cm;
    public Rigidbody rb;

    public float brakeStrength;
    public List<GameObject> tailLights;

    public UIManager uim;

    public enum DriveGear { Forward, Reverse, Off};
    public DriveGear driveGear = DriveGear.Off;

    public Lesson1Controller lesson1Controller;

    public AudioSource engineStart;
    public AudioSource engineIdle;
    public AudioSource thrustSound;
    public AudioSource waypointB;


    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if(cm)
        {
            rb.centerOfMass = new Vector3(0, -0.5f, 0.0f);
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
        }


        //Engine on/off
        if(Input.GetKeyDown(KeyCode.E) && driveGear == DriveGear.Off)
        {
            driveGear = DriveGear.Forward;
            StartCoroutine(EngineStart());
        }
        else if (Input.GetKeyDown(KeyCode.E) && (driveGear == DriveGear.Forward || driveGear == DriveGear.Reverse))
        {
            engineIdle.Stop();
            driveGear = DriveGear.Off;
        }

        //Reverse gear
        if(Input.GetKeyDown(KeyCode.R) && driveGear != DriveGear.Off)
        {
            driveGear = DriveGear.Reverse;
        }


        //Forward gear
        if (Input.GetKeyDown(KeyCode.F) && driveGear != DriveGear.Off)
        {
            driveGear = DriveGear.Forward;
        }


        if (Input.GetKeyDown(KeyCode.Tab) && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C1B)
        {
            StartCoroutine(SkipToC2());

        }
        uim.ChangeText(transform.InverseTransformVector(rb.velocity).z);
        uim.Gear(driveGear);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (WheelCollider wheelCollider in throttleWheels)
        {

            if (driveGear == DriveGear.Forward)
            {
                if(im.throttle > 0)
                {
                    //car moving forward
                    wheelCollider.motorTorque = strengthCoefficent * Time.deltaTime * im.throttle;
                    wheelCollider.brakeTorque = 0.0f;
                    if(!thrustSound.isPlaying) thrustSound.Play();

                }
                else
                {
                    thrustSound.Stop();
                    wheelCollider.motorTorque = 0.0f;
                    wheelCollider.brakeTorque = brakeStrength * Time.deltaTime;
                }
            }
            else if (driveGear == DriveGear.Reverse)
            {
                if (im.throttle > 0)
                {
                    //car moving backward

                    wheelCollider.motorTorque = strengthCoefficent * Time.deltaTime * -im.throttle;
                    wheelCollider.brakeTorque = 0.0f;
                    if (!thrustSound.isPlaying) thrustSound.Play();
 
                }
                else
                {
                    wheelCollider.motorTorque = 0.0f;
                    wheelCollider.brakeTorque = brakeStrength * Time.deltaTime;
                    thrustSound.Stop();
                }
            }

        }

        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0.0f, im.steer * maxTurn, 0.0f);
        }

        foreach (GameObject mesh in meshes)
        {
            //mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1.0f : -1.0f)
            //    / (2 * Mathf.PI * 0.34f), 0.0f, 0.0f);
        }

    }

    IEnumerator EngineStart()
    {
        // Play the first audio source
        if (!engineStart.isPlaying) engineStart.Play();
        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(engineStart.clip.length - 2.0f);
        // Play the second audio source
        if (!engineIdle.isPlaying) engineIdle.Play();

        yield return new WaitForSeconds(1.0f);
        if (!waypointB.isPlaying && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C1B)
        {
            waypointB.Play();
            uim.OldManActive();
        }


        yield return new WaitForSeconds(waypointB.clip.length);
        uim.OldManInActive();
        lesson1Controller.checkPoints = Lesson1Controller.CheckPoints.C2;
    }

    IEnumerator SkipToC2()
    {
        waypointB.Stop();
        yield return new WaitForSeconds(1.0f);
        lesson1Controller.checkPoints = Lesson1Controller.CheckPoints.C2;

    }
}
