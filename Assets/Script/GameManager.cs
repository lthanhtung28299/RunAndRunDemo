using System.Globalization;
using System.ComponentModel;
using System.Timers;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int Score = 0;
    int lives = 3;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject controlPlayerText;
    
    [Header("GameUI")]
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject gamePlayCanvas;
    [SerializeField] GameObject EndGameCanvas;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject resetGameButton;

    [Header("UISoundEffect")]
    [SerializeField] AudioClip UISoundFX;
    AudioSource adioS;

    void Awake()
    {
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if(numGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        adioS = GetComponent<AudioSource>();
        Time.timeScale = 0;
        LivesText(lives);
    }

    void Update()
    {
        LivesText(lives);
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

    public void StartGame()
    {
        controlPlayerText.SetActive(true);
        StartCoroutine(WalkThrought());
        adioS.PlayOneShot(UISoundFX);
        mainMenuCanvas.SetActive(false);
        gamePlayCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        adioS.PlayOneShot(UISoundFX);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
        mainMenuCanvas.SetActive(true);
        gamePlayCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
        Destroy(gameObject);
        Time.timeScale = 1;
    }


    public void GamePause()
    {
        adioS.PlayOneShot(UISoundFX);
        Time.timeScale = 0;
        resetGameButton.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        adioS.PlayOneShot(UISoundFX);
        Time.timeScale = 1;
        resetGameButton.SetActive(false);
        resumeButton.SetActive(false);    
        pauseButton.SetActive(true);
    }

    public void GameOver()
    {
        gamePlayCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
        StartCoroutine(StopGameRunning());
    }

    public void QuitGame()
    {
        adioS.PlayOneShot(UISoundFX);
        Application.Quit();
    }

    IEnumerator WalkThrought()
    {
        yield return new WaitForSecondsRealtime(5);
        controlPlayerText.SetActive(false);
    }

    IEnumerator StopGameRunning()
    {
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 0;
    }
}
