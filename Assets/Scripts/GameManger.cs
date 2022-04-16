using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    public enum eGameStates
    {
        StartScreen,
        Playing,
        Paused,
    }

    static eGameStates _gameState = eGameStates.StartScreen;
    static int Score;

    public delegate void DelStateChanged(eGameStates newState);
    public static event DelStateChanged StateChanged;

    public delegate void DelScoreCanged(int newScore);
    public static event DelScoreCanged ScoreChanged;


    public static void StartGame()
    {
        if(_gameState == eGameStates.StartScreen)
        {
            ResetScore();
            setState(eGameStates.Playing);
        }
    }

    public static void PauseGame()
    {
        if (_gameState == eGameStates.Playing)
        {
            setState(eGameStates.Paused);
        }
    }

    public static void ResumeGame()
    {
        if (_gameState == eGameStates.Paused)
        {
            setState(eGameStates.Playing);
        }
    }

    public static void StopGame()
    {
        setState(eGameStates.StartScreen);
    }

    public static void PlayerLost()
    {
        setState(eGameStates.StartScreen);
    }

    public static void PlayerWon()
    {
        setState(eGameStates.StartScreen);
    }


    public static void IncreaseScore()
    {
        Score++;
        ScoreChanged?.Invoke(Score);
    }

    private static void ResetScore()
    {
        Score = 0;
        ScoreChanged?.Invoke(Score);
    }


    private static void setState(eGameStates state)
    {
        if (_gameState != state)
        {
            _gameState = state;
            StateChanged?.Invoke(state);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}