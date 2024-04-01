using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public List<GameObject> springs;
    public float maxForce;
    public float maxDistance;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void FixedUpdate()
    {
        foreach(GameObject spring in springs)
        {
            RaycastHit hit;
            if(Physics.Raycast(spring.transform.position, -transform.up, out hit, maxDistance))//max distance equals spring equilibrium
            {
                rb.AddForceAtPosition(maxDistance * Time.fixedDeltaTime * transform.up * (maxDistance - hit.distance) /maxDistance, spring.transform.position);
            }
        }
    }
}
