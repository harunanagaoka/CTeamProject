using UnityEngine;

public class Score3 : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 3; // 獲得するスコアの値
    [SerializeField]
    private GameObject collectEffect; // 獲得エフェクトのPrefab

    private GameObject m_soundEffectManager = null;

    private SEManager m_SEManager = null;

    private void Awake()
    {
        m_soundEffectManager = GameObject.Find("SoundEffectManager");
        m_SEManager = m_soundEffectManager.GetComponent<SEManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        

        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.bCoin_get);
            GetScore(true);
            PlayEffect();
            Destroy(transform.parent.gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        if (other.CompareTag("Player2"))
        {
            m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.bCoin_get);
            GetScore(false);
            PlayEffect();
            Destroy(transform.parent.gameObject); // コインを消す
        }
        // 他にもスコアに応じた処理をここに記述可能
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
