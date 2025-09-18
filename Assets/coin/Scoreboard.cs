using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Text player2ScoreText;

    void Update()
    {
        player1ScoreText.text = "P1: " + Score1.player1Score.ToString();
        player2ScoreText.text = "P2: " + Score1.player2Score.ToString();
    }
}
