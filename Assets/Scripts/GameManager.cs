using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// GameManager : This class initiates to Play,Pause,GameOver and IncreaseScore.
/// </summary>

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    private int score;
    public GameObject playButton;
    public GameObject gameOver;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);


        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for(int i=0; i<pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }


        GameLogger.LogAction("Game Started");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        GameLogger.LogAction("Game Paused");
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
        GameLogger.LogAction("Game Over");
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }




}
