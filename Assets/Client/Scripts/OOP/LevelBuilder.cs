﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using LeopotamGroup.Globals;

public class LevelBuilder : MonoBehaviour
{
    #region Public Variables

    public GameProfiler Profiler;
    public Renderer BackgroundRender = null;

    #endregion

    #region Private Variables

    private Camera mainCamera = null;

    #endregion

    #region Unity Function

    private void Start()
    {
        mainCamera = Camera.main;

        CreateBlocks();

        CreatePaddle();

        CreateBall();
    }

    #endregion

    #region Private Function

    private void CreateBlocks()
    {
        // -------------------------------------------------
        // Create object

        Blocks blocks = new GameObject("[Blocks]", typeof(Blocks)).GetComponent<Blocks>();
        blocks.transform.SetParent(transform);

        // -------------------------------------------------

        blocks.Init(Profiler.TargetLevel, BackgroundRender.bounds);
    }

    private void CreatePaddle()
    {
        float posY = mainCamera.ScreenToWorldPoint(Vector2.zero).y + Profiler.HeightPaddle;

        // -------------------------------------------------
        // Create object

        var paddle = Instantiate(Profiler.PrefabPaddle, new Vector2(0, posY), Quaternion.identity, transform).GetComponent<Paddle>();
        paddle.name = "[PADDLE]";

        // -------------------------------------------------

        paddle.Init(Profiler.PaddleData);
    }

    private void CreateBall()
    {
        // -------------------------------------------------
        // Create object

        var ball = Instantiate(Profiler.PrefabBall, Vector2.zero, Quaternion.identity, transform).GetComponent<Ball>();
        ball.name = "[BALL]";

        // -------------------------------------------------

        ball.Init(Profiler.BallData);
    }

    #endregion
}
