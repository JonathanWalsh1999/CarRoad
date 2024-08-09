using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI lessonLogs;
    public GameObject lessonReport;
    public RawImage oldMan;

    public virtual void ChangeText(float speed)
    {
        float s = speed * 2.24694f; //convert m/s to mph

        speedText.text = Mathf.Abs(Mathf.Round(s)) + " MPH";
    }

    public virtual void Gear(CarController.DriveGear g)
    {
        if(g == CarController.DriveGear.Forward)
        {
            gearText.text = "Gear D";
        }
        else if (g == CarController.DriveGear.Reverse)
        {
            gearText.text = "Gear R";
        }
        else if (g == CarController.DriveGear.Off)
        {
            gearText.text = "Gear N";
        }

    }
    public virtual void UpdateSpeechText(string newText)
    {
        speechText.text = newText;
    }
    public virtual void SpeechTextActive()
    {
        speechText.gameObject.SetActive(true);
    }
    public virtual void SpeechTextInActive()
    {
        speechText.gameObject.SetActive(false);
    }

    public virtual void OldManActive()
    {

        oldMan.color = new Color(1, 1, 1, 1);
    }
    public virtual void OldManInActive()
    {
        oldMan.color = new Color(1, 1, 1, 0);
    }

    public virtual void UpdateLessonLogs(string log)
    {
        lessonLogs.text = "Logs: \n" + log;
    }
    public virtual void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }

    public virtual void LessonReportActive()
    {
        lessonReport.gameObject.SetActive(true);
    }
    public virtual void LessonReportInActive()
    {
        lessonReport.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Button was clicked!");
    }

    private void Update()
    {
        //fpsText.text = (Mathf.Round(1.0f / Time.deltaTime)).ToString() + " FPS";
    }

}
