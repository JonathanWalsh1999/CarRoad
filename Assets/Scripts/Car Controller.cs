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

    public List<GameObject> wheels;

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
            rb.centerOfMass = new Vector3(0, -0.5f, 0);
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

        uim.ChangeText(transform.InverseTransformVector(rb.velocity).z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1.0f : -1.0f) 
                / (2 * Mathf.PI * 0.34f), 0.0f, 0.0f);
        }



    }
}
