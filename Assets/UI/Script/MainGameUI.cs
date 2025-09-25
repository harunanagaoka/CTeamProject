using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_timerText = null;

    [SerializeField]
    private ScoreManager m_scoreManager = null;

    [SerializeField]
    private TextMeshProUGUI m_scoreText_one = null;

    [SerializeField]
    private TextMeshProUGUI m_scoreText_two = null;

    private MainGameTimer m_timer = null;

    void Start()
    {
        m_timer = GetComponent<MainGameTimer>();
    }

    void Update()
    {
        UpdateTimerText();
        UpdateScoreText();
    }

    private void UpdateTimerText()
    {
        int currentTime = (int)m_timer.CurrentTime;
        m_timerText.text = currentTime.ToString();
    }

    private void UpdateScoreText()
    {
        m_scoreText_one.text = m_scoreManager.PlayerOneScore.ToString();
        m_scoreText_two.text = m_scoreManager.PlayerTwoScore.ToString();
    }
}
