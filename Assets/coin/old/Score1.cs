using UnityEngine;

public class Score1 : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 1; // コインのスコア値

    [SerializeField]
    private GameObject collectEffect; // 獲得エフェクトのPrefab


    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            GetScore();
            PlayEffect();
            Destroy(gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        if (other.CompareTag("Player2"))
        {
            GetScore();
            PlayEffect();
            Destroy(gameObject); // コインを消す
        }
    }

    private void GetScore()
    {
        ScoreManager score = GetComponentInParent<ScoreManager>();
        score.AddScore(true, scoreValue);
    }

    private void PlayEffect()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }
}
