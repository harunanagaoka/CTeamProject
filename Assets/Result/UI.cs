using UnityEngine;
using UnityEngine.UI; // ’Ç‰Á

public class UI : MonoBehaviour
{
    public static int player1Score;
    public static int player2Score;

    [SerializeField]
    private Text winnerText; // Inspector‚ÅText‚ðƒAƒTƒCƒ“

    void Update()
    {
    }


    void ShowWinner()
    {
        int p1 = Score1.player1Score;
        int p2 = Score1.player2Score;

        if (p1 > p2)
        {
            winnerText.text = "Player 1 wins!";
        }
        else if (p2 > p1)
        {
            winnerText.text = "Player 2 wins!";
        }
        else
        {
            winnerText.text = "It's a tie!";
        }
    }
}
