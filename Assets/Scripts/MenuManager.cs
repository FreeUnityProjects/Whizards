using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    VisualElement ve;
    Label lblScore;
    GameManager.eGameStates oldState;
    public bool startImmediately;

    private void OnEnable()
    {
        ve = GetComponent<UIDocument>().rootVisualElement;

        var buttons = ve.Query<Button>();
        foreach (var btn in buttons.ToList())
        {
            btn.RegisterCallback<ClickEvent>((evt) => buttonClicked(btn,evt));
        }

        lblScore = ve.Q<Label>("score");
        // Debug.Assert(lblScore);

        lblScore.text = "0";

        GameManager.StateChanged += GameManager_StateChanged;
        GameManager.ScoreChanged += GameManager_ScoreChanged;

        if (startImmediately)
        {
            GameManager.StartGame();
        }
    }

    private void GameManager_ScoreChanged(int newScore)
    {
            lblScore.text = newScore + "";
    }

    private void OnDisable()
    {
        GameManager.StateChanged -= GameManager_StateChanged;
        GameManager.ScoreChanged -= GameManager_ScoreChanged;
    }

    private void GameManager_StateChanged(GameManager.eGameStates newState)
    {
        if(newState == GameManager.eGameStates.Playing && oldState == GameManager.eGameStates.StartScreen)
        {
            ve.Q<VisualElement>("StartScreenBackground").style.display = DisplayStyle.None;
        }
        else if (newState == GameManager.eGameStates.StartScreen)
        {
            ve.Q<VisualElement>("StartScreenBackground").style.display = DisplayStyle.Flex;
        }

        oldState = newState;
    }

    private void buttonClicked(Button sender ,ClickEvent evt)
    {
        Debug.Log("clicked: " + sender.name);

        switch (sender.name)
        {
            case "cmd_play":
                GameManager.StartGame();
                break;
            case "cmd_pause":
                GameManager.PauseGame();
                break;
            case "cmd_openOption":
                GameManager.PauseGame();
                ve.Q<VisualElement>("OptionsMenu").style.display = DisplayStyle.Flex;
                ve.Q<VisualElement>("StartScreenBackground").style.display = DisplayStyle.None;
                break;
            case "cmd_closeOption":
                GameManager.ResumeGame();
                ve.Q<VisualElement>("OptionsMenu").style.display = DisplayStyle.None;
                if (oldState == GameManager.eGameStates.StartScreen)
                {
                    ve.Q<VisualElement>("StartScreenBackground").style.display = DisplayStyle.Flex;
                }
                break;
            case "cmd_close":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
        }
    }
}
