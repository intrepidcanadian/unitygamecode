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

    [ContextMenu("Add Score")]
    public void addScore(int scoreToAdd) {

        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    [ContextMenu("Restart Score")]
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    private void Start()
    {
        
    }
    
    
 
}
