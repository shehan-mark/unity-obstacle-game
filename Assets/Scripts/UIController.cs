using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    BeforeStart,
    Running,
    Paused,
    GameOver
}

public class UIController : MonoBehaviour
{
    public float Score;
    public GameObject MenuPanel;
    public GameObject StartButton, ResumeButton, RestartButton, GameOverLabel;
    public GameObject PlayerObj;
    public TMP_Text ScoreValuePlaceholder;

    private static GameState State;

    // Start is called before the first frame update
    void Start()
    {
        State = GameState.BeforeStart;
        StartButton.SetActive(false);
        ResumeButton.SetActive(false);
        RestartButton.SetActive(false);
        GameOverLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //print($"Game State {State} and timescale {Time.timeScale}");
        if (State == GameState.BeforeStart && !StartButton.activeSelf)
        {
            StartButton.SetActive(true);
            MenuPanel.SetActive(true);
        }

        if (State == GameState.Paused && !ResumeButton.activeSelf)
        {
            ResumeButton.SetActive(true);
            MenuPanel.SetActive(true);
        }

        if (State == GameState.GameOver && !RestartButton.activeSelf)
        {
            GameOverLabel.SetActive(true);
            RestartButton.SetActive(true);
            MenuPanel.SetActive(true);
        }


        // reference to pause and unpause games - https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
        if (State != GameState.GameOver && State != GameState.BeforeStart && Input.GetKeyDown(KeyCode.Escape))
        {
            if (State != GameState.Paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        ToggleMenuPanel();
        StartButton.SetActive(false);
        SpawnPlayer();
        State = GameState.Running;
    }

    public void PauseGame()
    {
        print("LOG:: PAUSE");
        Time.timeScale = 0;
        State = GameState.Paused;
    }

    public void ResumeGame()
    {
        print("LOG:: UN-PAUSE");
        Time.timeScale = 1;
        ToggleMenuPanel();
        ResumeButton.SetActive(false);
        State = GameState.Running;
    }

    public void RestartGame()
    {
        Score = 0.0f;
        UpdateScoreUI();
        SpawnPlayer();
        ToggleMenuPanel();
        RestartButton.SetActive(false);
        GameOverLabel.SetActive(false);
        State = GameState.Running;
    }

    public void SetGameState(GameState IncomingState)
    {
        State = IncomingState;
    }

    public GameState GetGameState()
    {
        return State;
    }

    public void SetScore(int IncomingScore)
    {
        Score = Score + IncomingScore;
        UpdateScoreUI();
    }

    private void ToggleMenuPanel()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerObj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(90, 0, 0));
    }

    private void UpdateScoreUI()
    {
        ScoreValuePlaceholder.text = Score.ToString();
    }
}
