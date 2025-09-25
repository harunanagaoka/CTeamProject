using UnityEngine;

public class Score1 : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 1; // コインのスコア値

    [SerializeField]
    private GameObject collectEffect; // 獲得エフェクトのPrefab

    private GameObject m_soundEffectManager = null;

    private SEManager m_SEManager = null;

    private void Awake()
    {
        m_soundEffectManager = GameObject.Find("SoundEffectManager");
        m_SEManager = m_soundEffectManager.GetComponent<SEManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.nCoin_get);
            GetScore(true);
            PlayEffect();
            Destroy(transform.parent.gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        if (other.CompareTag("Player2"))
        {
            m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.nCoin_get);
            GetScore(false);
            PlayEffect();
            Destroy(transform.parent.gameObject); // コインを消す
        }
    }

    private void GetScore(bool isPlayerOne)
    {
        ScoreManager score = GetComponentInParent<ScoreManager>();
        score.AddScore(isPlayerOne, scoreValue);
    }

    private void PlayEffect()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }
}
