using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public virtual void ChangeText(float speed)
    {
        float s = speed * 2.24694f; //convert m/s to mph

        text.text = Mathf.Abs(Mathf.Round(s)) + " MPH";
    }

}
