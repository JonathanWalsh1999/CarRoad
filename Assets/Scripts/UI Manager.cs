using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI fpsText;

    public virtual void ChangeText(float speed)
    {
        float s = speed * 2.24694f; //convert m/s to mph

        speedText.text = Mathf.Abs(Mathf.Round(s)) + " MPH";
    }

    private void Update()
    {
        fpsText.text = (Mathf.Round(1.0f / Time.deltaTime)).ToString() + " FPS";
    }

}
