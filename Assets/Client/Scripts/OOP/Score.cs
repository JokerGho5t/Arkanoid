using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JokerGho5t.MessageSystem;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    private TextMeshProUGUI ScoreText = null;
    private int score = 0;

    private void Start()
    {

        ScoreText = GetComponent<TextMeshProUGUI>();

        Restart();

        Message.AddListener("RestartLevel", Restart);
        Message.AddListener("RemoveBrick", AddScore);
    }

    private void AddScore()
    {
        if (score == 0)
            ScoreText.alignment = TextAlignmentOptions.MidlineLeft;

        score++;
        ScoreText.text = "Score: " + score;
    }

    private void Restart()
    {
        score = 0;

        ScoreText.text = "Score";
        ScoreText.alignment = TextAlignmentOptions.Midline;
    }
}
