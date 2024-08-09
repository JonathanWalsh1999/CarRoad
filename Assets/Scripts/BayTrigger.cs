using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayTrigger : MonoBehaviour
{
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
    public void SetInActive()
    {
        gameObject.SetActive(false);
    }
}
