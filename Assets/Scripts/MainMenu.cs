using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //load iceman
        SceneManager.LoadScene("template scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
