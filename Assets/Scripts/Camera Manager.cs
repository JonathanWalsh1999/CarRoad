using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float dampening = 1.0f;

    public float h2 = 0.0f;
    public float d2 = 0.0f;
    public float l = 0.0f;

    private int camMode = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = (camMode + 1) % 3; //2 = max num of cameras
        }

        switch (camMode)
        {
            case 1:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0.0f, height, -distance));
                transform.LookAt(focus.transform); 
                Camera.main.fieldOfView = 60.0f;
                break;

            case 2:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(l, h2, d2));
                transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 60.0f;
                break;

            default:
                transform.position = Vector3.Lerp(transform.position, focus.transform.position +
                    focus.transform.TransformDirection(new Vector3(0.0f, height, -distance)), dampening * Time.deltaTime);
                transform.LookAt(focus.transform);
                Camera.main.fieldOfView = 60.0f;
                break;
        }
    }
}
