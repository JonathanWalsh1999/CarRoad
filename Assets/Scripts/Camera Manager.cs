using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float dampening = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, focus.transform.position + 
            focus.transform.TransformDirection(new Vector3(0.0f, height, -distance)), dampening * Time.deltaTime);
        transform.LookAt(focus.transform);
    }
}
