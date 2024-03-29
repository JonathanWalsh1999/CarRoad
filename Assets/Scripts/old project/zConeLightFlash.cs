using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeLightFlash : MonoBehaviour
{
    [SerializeField]
    private GameObject lightPP;

    [SerializeField]
    private float offset = 0.0f;

    [SerializeField]
    private float speed = 5.0f;


    private float time;
    private bool isOn = false;

    // Start is called before the first frame update
    void Awake()
    {
        time = speed + offset;
    }

    // Update is called once per frame
    void Update()
    {

        if(isOn)
        {
            lightPP.SetActive(true);
            time -= speed * Time.deltaTime;

            if(time < 0)
            {
                isOn = false;
                time = speed;
            }
        }
        else
        {
            lightPP.SetActive(false);
            time -= speed * Time.deltaTime;

            if (time < 0)
            {
                isOn = true;
                time = speed;
            }
        }
        
    }
}
