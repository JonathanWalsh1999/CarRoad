using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource bgMusic;
    public float fadeDuration = 2.0f; // Duration of the fade-out in seconds
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel1()
    {
       StartCoroutine(FadeOutMusicAndLoadScene(1));
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutMusicAndLoadScene(int sceneName)
    {
        float startVolume = bgMusic.volume;

        while (bgMusic.volume > 0)
        {
            bgMusic.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        bgMusic.Stop();
        bgMusic.volume = startVolume; // Reset the volume for future use

        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }
}
