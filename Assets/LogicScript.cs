using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public BirdScript bird;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.enabled = true;
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd) {
        if (bird.birdIsAlive)
        {
            playerScore+= scoreToAdd;
        }
        scoreText.text = playerScore.ToString();
    }

    public void restartGame() {
        SceneManager.LoadScene(1);
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
    }
}
