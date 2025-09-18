using UnityEngine;

public class Score3 : MonoBehaviour
{
    // 例: スコアの変化を監視してログに出力
    private int lastPlayer1Score = -1;
    private int lastPlayer2Score = -1;

    void Start()
    {
        
    }

    void Update()//debug用
    {
        // スコアが変化した場合にログを出力
        if (lastPlayer1Score != Score1.player1Score)
        {
            lastPlayer1Score = Score1.player1Score;
            Debug.Log("Player 1 Score: " + lastPlayer1Score);
        }
        if (lastPlayer2Score != Score1.player2Score)
        {
            lastPlayer2Score = Score1.player2Score;
            Debug.Log("Player 2 Score: " + lastPlayer2Score);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        // Score1のスコアを常に参照
        int p1 = Score1.player1Score;
        int p2 = Score1.player2Score;


        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            p1 += 3;
            Destroy(gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        if (other.CompareTag("Player2"))
        {
            p2 += 3;
            Destroy(gameObject); // コインを消す
        }
        // 他にもスコアに応じた処理をここに記述可能
    }
}
