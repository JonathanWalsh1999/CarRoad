using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1Controller : MonoBehaviour
{
    public enum CheckPoints { C1,C1B, C2,C3,C3B,C4,C5 };
    public CheckPoints checkPoints;

    public List<AudioSource> sources;

    public UIManager uim;

    public int points = 0;
    public string log = "";
        

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = CheckPoints.C1;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPoints == CheckPoints.C1)
        {
            if (!sources[0].isPlaying)
            {
                sources[0].Play();
                uim.OldManActive();
                StartCoroutine(FinishedC1());

            }
        }
        else if (checkPoints == CheckPoints.C1B)
        {
            points += 5;
        }
        else if (checkPoints == CheckPoints.C2)
        {
            points += 5;
        }
        else if (checkPoints == CheckPoints.C3)
        {
            points += 5;
        }
        else if (checkPoints == CheckPoints.C3B)
        {
            points += 5;
        }
        else if (checkPoints == CheckPoints.C4)
        {
            points += 5;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && checkPoints == CheckPoints.C1)
        {
            StartCoroutine(SkipToC1B());
        }
    }


    IEnumerator FinishedC1()
    {
        yield return new WaitForSeconds(sources[0].clip.length - 2.0f);
        uim.OldManInActive();
        checkPoints = CheckPoints.C1B;
    }

    IEnumerator SkipToC1B()
    {
        sources[0].Stop();
        yield return new WaitForSeconds(1.0f);
        checkPoints = CheckPoints.C1B;

    }
}
