using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LogicScript : MonoBehaviour
{
    private float playTime;
    public GameObject startText;
    public float elapsedTime = 0;
    public int playerScore;
    public bool isGameOver;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    public bool isPaused = false;
    private float scoreMod;
    public bool canRevive;
    public bool isStarted;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        scoreMod = 1f;
        canRevive = false;
        StartCoroutine(WaitForInput());
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (playTime < 10)
        {
            playTime += Time.deltaTime;
        }
        else {
            MultiplyScoreMod(1.05f);
            playTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGameOver) TogglePause();
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore += Mathf.RoundToInt(scoreToAdd*scoreMod);
        scoreText.text = playerScore.ToString();
    }
    public void MultiplyScoreMod(float amount)
    {
        scoreMod *= amount;
    }

    public void restartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;  // Pause the game
            pauseMenu.SetActive(true);  // Show the pause menu
        }
        else
        {
            Time.timeScale = 1f;  // Resume the game
            pauseMenu.SetActive(false);  // Hide the pause menu
        }
    }

    private IEnumerator WaitForInput()
    {
        // Show the start text or any UI element indicating to the player to press a button
        startText.SetActive(true);
        isStarted=false;

        // Wait until the player inputs any key or button
        while (!Input.GetButtonDown("Jump"))
        {
            yield return null;
        }

        // Hide the start text or UI element
        startText.SetActive(false);

        // Continue with the game or scene logic
        StartGame();
    }

    private void StartGame()
    {
        Destroy(startText);
        Time.timeScale = 1f;
        isStarted = true;
    }

    
}
