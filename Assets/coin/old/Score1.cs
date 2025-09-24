using UnityEngine;

public class Score1 : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 1; // コインのスコア値

    [SerializeField]
    private GameObject collectEffect; // 獲得エフェクトのPrefab

    // プレイヤーごとのスコア
    public static int player1Score = 0;
    public static int player2Score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            player1Score += scoreValue;
            PlayEffect();
            Destroy(gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        if (other.CompareTag("Player2"))
        {
            player2Score += scoreValue;
            PlayEffect();
            Destroy(gameObject); // コインを消す
        }
    }

    private void PlayEffect()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }
}
