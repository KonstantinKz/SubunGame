using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas aboutMenu;
    public Text subun;
    public Canvas story;
    public Canvas howToPlay;

    public void Play()   
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("Game");
    }

    public void HighScores()
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("HighScores");
    }

    public void Quit()
    {
        Debug.Log("Closed");
        Application.Quit();
    }

    public void About()
    {
        mainMenu.enabled = false;
        aboutMenu.enabled = true;
    }

    public void QuitFromAbout()
    {
        aboutMenu.enabled = false;
        mainMenu.enabled = true;
    }
    
    public void HowToPlay()
    {
        aboutMenu.enabled = false;
        howToPlay.enabled = true;
    }

    public void Story()
    {
        aboutMenu.enabled = false;
        subun.enabled = false;
        story.enabled = true;
    }

    public void Back()
    {
        aboutMenu.enabled = true;
        subun.enabled = true;
        story.enabled = false;
        howToPlay.enabled = false;
    }
}
