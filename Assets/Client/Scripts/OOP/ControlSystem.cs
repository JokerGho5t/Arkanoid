using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JokerGho5t.MessageSystem;
using UnityEditor;

public class ControlSystem : MonoBehaviour
{
#if UNITY_EDITOR
    public bool CheckFocus = true;
#endif

    #region Private Variables

    private bool isPause = false;
    private bool isStart = false;
    private bool isWin = false;

    #endregion

    #region Unity Function

    private void Start()
    {
        Message.AddListener("CloseGame", CloseGame);
        Message.AddListener("PauseGame", PauseGame);
        Message.AddListener("WinLevel", Win);
    }

    private void Update()
    {
        Message.Send("OnUpdate");

        if (isWin && Input.GetKeyDown(KeyCode.Space))
            Message.Send("RestartLevel");

        if (!isStart && Input.GetKeyDown(KeyCode.Space))
            Message.Send("StartLevel");
    }

    private void FixedUpdate()
    {
        Message.Send("OnFixedUpdate");
    }

    private void OnDestroy()
    {
        Message.RemoveAllListener();
    }

    private void OnApplicationFocus(bool focus)
    {
#if UNITY_EDITOR
        if (!CheckFocus)
            return;
#endif

        if (!isPause)
            Time.timeScale = (!focus) ? 0 : 1;
    }

    #endregion

    #region Private Function

    private void Win()
    {
        isWin = true;
    }

    private void PauseGame()
    {
        isPause = !isPause;

        Time.timeScale = (isPause)? 0 : 1;
    }

    private void CloseGame()
    {
#if UNITY_EDITOR
        Debug.Break();
#else
        Application.Quit();
#endif
    }

    #endregion
}