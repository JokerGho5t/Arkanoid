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

    private bool isPause = false;

    private void Start()
    {
        Message.AddListener("CloseGame", CloseGame);
        Message.AddListener("PauseGame", PauseGame);
    }

    void Update()
    {
        Message.Send("OnUpdate");
    }

    private void FixedUpdate()
    {
        Message.Send("OnFixedUpdate");
    }

    private void OnDestroy()
    {
        Message.RemoveAllListener();
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

    private void OnApplicationFocus(bool focus)
    {
#if UNITY_EDITOR
        if (!CheckFocus)
            return;
#endif

        if (!isPause)
            Time.timeScale = (!focus)? 0 : 1;
    }
}
