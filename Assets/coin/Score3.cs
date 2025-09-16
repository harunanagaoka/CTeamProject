using UnityEngine;

public class Score3 : MonoBehaviour
{
    // 例: スコアの変化を監視してログに出力
    private int lastPlayer1Score = -1;
    private int lastPlayer2Score = -1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Score1のスコアを常に参照
        int p1 = Score1.player1Score;
        int p2 = Score1.player2Score;

        // スコアが変化したときだけログ出力（例）
        if (p1 != lastPlayer1Score || p2 != lastPlayer2Score)
        {
            Debug.Log($"Player1 Score: {p1}, Player2 Score: {p2}");
            lastPlayer1Score = p1;
            lastPlayer2Score = p2;
        }

        // 他にもスコアに応じた処理をここに記述可能
    }
}
