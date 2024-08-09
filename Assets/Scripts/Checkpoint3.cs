using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Lesson1Controller;

public class Checkpoint3 : MonoBehaviour
{
    public AudioSource checkpoint3;
    public AudioSource petrolPump;
    public AudioSource checkpoint3B;

    public AudioSource engineStart;
    public AudioSource engineIdle;

    public Lesson1Controller lesson1Controller;
    public GameObject highlight;

    public CarController carController;

    public UIManager uim;


    private bool entered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (entered && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C3)
        {
            highlight.SetActive(false);
            StartCoroutine(CP3());

            entered = false;
            
        }
        else if (!entered && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C3)
        {
            highlight.SetActive(true);
        }

        if (lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C3B)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(CP3B());

            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C3)
        {
            StartCoroutine(SkipToC3B());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
            Debug.Log(lesson1Controller.checkPoints);
        }
    }

    IEnumerator CP3()
    {


        // Play the first audio source
        if (!checkpoint3.isPlaying) checkpoint3.Play(); uim.OldManActive();
        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(checkpoint3.clip.length);

        uim.OldManInActive();
        highlight.SetActive(false);
        lesson1Controller.checkPoints = Lesson1Controller.CheckPoints.C3B;

    }

    IEnumerator CP3B()
    {
        carController.driveGear = CarController.DriveGear.Off;
        engineStart.Stop();
        engineIdle.Stop();

        if (!petrolPump.isPlaying) petrolPump.Play();
        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(petrolPump.clip.length);

        if (!checkpoint3B.isPlaying) checkpoint3B.Play();
        if(!engineStart.isPlaying) engineStart.Play();
        if(!engineIdle.isPlaying) engineIdle.Play();
        carController.driveGear = CarController.DriveGear.Forward;

        yield return new WaitForSeconds(2.0f);
        if (!checkpoint3B.isPlaying) checkpoint3B.Play();
        lesson1Controller.checkPoints = Lesson1Controller.CheckPoints.C4;

    }

    IEnumerator SkipToC3B()
    {
        checkpoint3.Stop();
        yield return new WaitForSeconds(1.0f);
        lesson1Controller.checkPoints = CheckPoints.C3B;
    }
}
