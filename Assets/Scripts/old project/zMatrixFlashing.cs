using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixFlashing : MonoBehaviour
{
    [SerializeField]
    private GameObject top;

    [SerializeField]
    private GameObject bottom;


    [SerializeField]
    private float speed = 5.0f;


    private float time;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        time = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            top.SetActive(true);
            bottom.SetActive(false);
            time -= speed * Time.deltaTime;

            if (time < 0)
            {
                isOn = false;
                time = speed;
            }
        }
        else
        {
            top.SetActive(false);
            bottom.SetActive(true);
            time -= speed * Time.deltaTime;

            if (time < 0)
            {
                isOn = true;
                time = speed;
            }
        }
    }
}
