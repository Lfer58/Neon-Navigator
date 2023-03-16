using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScree : MonoBehaviour
{
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void playGame() {
        titleScreen.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
