using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using JokerGho5t.ScriptableObjects;

public class GameProfiler : SerializedScriptableObject
{
    public GameObject PrefabPaddle => _prefabPaddle;
    [TitleGroup("Paddle Setting")]
    [SerializeField]
    private GameObject _prefabPaddle = null;

    public float HeightPaddle => _heightPaddle;
    [TitleGroup("Paddle Setting")]
    [SerializeField]
    private float _heightPaddle = 1;

    public PaddleComponent PaddleData => _paddleData;
    [TitleGroup("Paddle Setting")]
    [SerializeField]
    private PaddleComponent _paddleData = new PaddleComponent();

    public GameObject PrefabBall => _prefabBall;
    [TitleGroup("Ball Setting")]
    [SerializeField]
    private GameObject _prefabBall = null;

    public BallComponent BallData => _ballData;
    [TitleGroup("Ball Setting")]
    [SerializeField]
    private BallComponent _ballData = new BallComponent();

    public LevelData TargetLevel => _targetLevel;
    [TitleGroup("Target Level")]
    [SerializeField, Required, HideLabel]
    [InlineEditor]
    private LevelData _targetLevel = null;
}

[System.Serializable]
public struct BallComponent
{
    public float SpeedBall;
}

[System.Serializable]
public struct PaddleComponent
{
    public float SpeedPaddle;
}

[System.Serializable]
public struct BlockComponent
{
    public BlockComponent(Sprite sprite, int life)
    {
        this.sprite = sprite;

        this.life = (life < 1) ? 1 : life;
    }

    [Min(1)]
    public int life;
    public Sprite sprite;
}