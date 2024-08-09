using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lesson1Controller;

public class Checkpoint2 : MonoBehaviour
{
    public AudioSource checkpoint2;
    public AudioSource checkpoint2b;
    public Lesson1Controller lesson1Controller;

    public GameObject highlight;

    public UIManager uim;


    private bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(entered && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C2)
        {

            
            
            StartCoroutine(CP2());

            entered = false;

    

        }
        else if(lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C2)
        {
            highlight.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Tab) && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C2)
        {

            StartCoroutine(SkipToC3());


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    IEnumerator CP2()
    {

        // Play the first audio source
        if (!checkpoint2.isPlaying && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C2) checkpoint2.Play(); uim.OldManActive();

        // Wait until the first audio source finishes playing
        yield return new WaitForSeconds(checkpoint2.clip.length);
        // Play the second audio source
        if (!checkpoint2b.isPlaying && lesson1Controller.checkPoints == Lesson1Controller.CheckPoints.C2) checkpoint2b.Play();

        yield return new WaitForSeconds(checkpoint2b.clip.length);

        uim.OldManInActive();
        lesson1Controller.checkPoints = Lesson1Controller.CheckPoints.C3;
        highlight.SetActive(false);
        //gameObject.SetActive(false);

    }

    IEnumerator SkipToC3()
    {
        checkpoint2.Stop();
        lesson1Controller.checkPoints = CheckPoints.C3;

        //gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

    }
}
