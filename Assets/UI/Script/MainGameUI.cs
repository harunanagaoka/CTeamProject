using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_timerText = null;

    private MainGameTimer m_timer = null;

    void Start()
    {
        m_timer = GetComponent<MainGameTimer>();
        int a = (int)m_timer.CurrentTime;
    }

    void Update()
    {
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int currentTime = (int)m_timer.CurrentTime;

        m_timerText.text = currentTime.ToString();
    }

    private void UpdateScore()
    {

    }
}
