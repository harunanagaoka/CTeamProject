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

    private Canvas m_mainCanvas = null;

    private MainGameTimer m_timer = null;

    private const string m_timerObjectName = "MainGameManager";

    private void Awake()
    {
        m_mainCanvas = GetComponent<Canvas>();

        GameObject timerObject = GameObject.Find(m_timerObjectName);
        m_timer = timerObject.GetComponent<MainGameTimer>();
    }

    private void Update()
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

    public void SetCanvasVisible(bool visible)
    {
        m_mainCanvas.enabled = visible;
    }
}
