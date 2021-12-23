using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int Score = 0;
    int lives;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject gamePlayCanvas;
    [SerializeField] GameObject EndGameCanvas;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject resetGameButton;
    [SerializeField] GameObject backMainMenuButton;
    [SerializeField] AudioClip UISoundFX;
    AudioSource adioS;

    void Start()
    {
        adioS = GetComponent<AudioSource>();
        resetGameButton.SetActive(false);
        resumeButton.SetActive(false);
        backMainMenuButton.SetActive(false);
        pauseButton.SetActive(true);
        EndGameCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = $"Score: {Score.ToString()}";
        finalScoreText.text = $"Score: {Score.ToString()}";
    }

    public void LivesText(int playerLive)
    {
        lives = playerLive;
        livesText.text = $"Live: {lives.ToString()}";
    }

    public void ResetGame()
    {
        adioS.PlayOneShot(UISoundFX);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
        Time.timeScale = 1;
        resetGameButton.SetActive(false);
        resumeButton.SetActive(false);
        backMainMenuButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        adioS.PlayOneShot(UISoundFX);
        SceneManager.LoadScene(0);
        gamePlayCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void GamePause()
    {
        adioS.PlayOneShot(UISoundFX);
        Time.timeScale = 0;
        resetGameButton.SetActive(true);
        resumeButton.SetActive(true);
        backMainMenuButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        adioS.PlayOneShot(UISoundFX);
        Time.timeScale = 1;
        resetGameButton.SetActive(false);
        resumeButton.SetActive(false);
        backMainMenuButton.SetActive(false);        
        pauseButton.SetActive(true);
    }

    public void GameOver()
    {
        gamePlayCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
    }
}
