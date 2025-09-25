using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // プレイヤーごとのスコア
    private int playerScore_One = 0;
    private int playerScore_Two = 0;

    public int PlayerOneScore { get { return playerScore_One; } private set { playerScore_One = value; } }
    public int PlayerTwoScore { get { return playerScore_Two; } private set { playerScore_Two = value; } }

    public void AddScore (bool isPlayerOne,int score)
    {
        if (isPlayerOne)
        {
            playerScore_One += score;
        }
        else
        {
            playerScore_Two += score;
        }
    }

    public void ResetScore()
    {
        playerScore_One = 0;
        playerScore_Two = 0;
        DestroyAllChildren();
    }

    private void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
