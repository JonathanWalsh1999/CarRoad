using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lesson1Controller;

public class Checkpoint4 : MonoBehaviour
{
    public BayTrigger correctBay;
    public BayTrigger wrongBayLeft;
    public BayTrigger wrongBayRight;
    public BayTrigger reversed;

    public AudioSource success;
    public AudioSource fail;

    public GameObject highlight;

    public Lesson1Controller controller;

    public UIManager uim;



    // Start is called before the first frame update
    void Start()
    {
        reversed.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((wrongBayLeft.triggered || wrongBayRight.triggered) && controller.checkPoints == Lesson1Controller.CheckPoints.C4)
        {
            wrongBayLeft.triggered = false;
            wrongBayRight.triggered = false;

            correctBay.SetInActive();
            wrongBayLeft.SetInActive();
            wrongBayRight.SetInActive();

            StartCoroutine(CP4Fail());
            reversed.SetActive();

        }
        else if (controller.checkPoints == Lesson1Controller.CheckPoints.C4)
        {
            highlight.SetActive(true);
        }

        if(correctBay.triggered && controller.checkPoints == Lesson1Controller.CheckPoints.C4)
        {
            highlight.SetActive(false);
            correctBay.SetInActive();
            wrongBayLeft.SetInActive();
            wrongBayRight.SetInActive();
            StartCoroutine(CP4());

            correctBay.triggered = false;
        }

        if (reversed.triggered && controller.checkPoints == Lesson1Controller.CheckPoints.C4)
        {
            reversed.SetInActive();
            correctBay.SetActive();
            wrongBayLeft.SetActive();
            wrongBayRight.SetActive();

            reversed.triggered = false;
        }
    }

    IEnumerator CP4()
    {
        //Success
 

        if (!success.isPlaying) success.Play(); uim.OldManActive();
        uim.UpdateLessonLogs(controller.log);
        uim.UpdatePoints(controller.points);
        uim.LessonReportActive();
        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(success.clip.length);

        uim.OldManInActive();
        controller.checkPoints = Lesson1Controller.CheckPoints.C5;

    }

    IEnumerator CP4Fail()
    {
        if (!fail.isPlaying) fail.Play(); uim.OldManActive();  controller.log += "You did not park in the bay correctly!\n";
        controller.points -= 5;

        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(fail.clip.length);
        uim.OldManInActive();

    }
}
