using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour
{
    const int maxScore = 100000;
    int s; public int Score { get => s; set => s = Mathf.Clamp(value, 0, maxScore); }

    public Text scoreText;

    private void Start()
    {
        DisplayScore();
    }

    public void _AddScore(int value)
    {
        Score += value;
        DisplayScore();
    }
    public void _RemoveScore(int value)
    {
        Score -= value;
        DisplayScore();
    }

    public void DisplayScore()
    {
        scoreText.text = Score.ToString();
    }
}
