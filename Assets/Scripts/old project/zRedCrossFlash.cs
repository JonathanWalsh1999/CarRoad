using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrossFlash : MonoBehaviour
{
    [SerializeField]
    private GameObject leftSide;

    [SerializeField]
    private GameObject rightSide;


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
            leftSide.SetActive(true);
            rightSide.SetActive(false);
            time -= speed * Time.deltaTime;

            if (time < 0)
            {
                isOn = false;
                time = speed;
            }
        }
        else
        {
            leftSide.SetActive(false);
            rightSide.SetActive(true);
            time -= speed * Time.deltaTime;

            if (time < 0)
            {
                isOn = true;
                time = speed;
            }
        }

    }
}
