using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Text player2ScoreText;
    [SerializeField] private Font Font;

    void Start()
    {
        if (Font != null)
        {
            if (player1ScoreText != null) player1ScoreText.font = Font;
            if (player2ScoreText != null) player2ScoreText.font = Font;
        }
    }

    void Update()
    {
        player1ScoreText.text = "P1: " + Score1.player1Score.ToString();
        player2ScoreText.text = "P2: " + Score1.player2Score.ToString();
    }
}
