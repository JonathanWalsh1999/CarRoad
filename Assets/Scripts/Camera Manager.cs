using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float dampening = 1.0f;

    public float h2 = 0.0f;
    public float d2 = 0.0f;
    public float l = 0.0f;

    private int camMode = 1;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    camMode = (camMode + 1) % 3; //2 = max num of cameras
        //}

        switch (camMode)
        {
            case 1:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0.0f, height, -distance));
                transform.LookAt(focus.transform); 
                Camera.main.fieldOfView = 60.0f;

                if (Input.GetKey(KeyCode.Keypad6))
                {
                    transform.Rotate(new Vector3(0, 90.0f, 0));
                    transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0.0f, 0, -distance));
                }
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    transform.Rotate(new Vector3(0, -90.0f, 0));
                    transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0.0f, 0, -distance));
                }
                if (Input.GetKey(KeyCode.Keypad2))
                {
                    transform.Rotate(new Vector3(0, 180.0f, 0));
                    transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0.0f, 0, -distance));
                }


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
