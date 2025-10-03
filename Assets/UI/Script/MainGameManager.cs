using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private MainGameTimer m_timer = null;

    private ScoreManager m_scoreManager = null;

    private Loopinterval m_coinSpawnner = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_timer = GetComponent<MainGameTimer>();
        m_scoreManager = GetComponent<ScoreManager>();
        m_coinSpawnner = GetComponent<Loopinterval>();
        ResetMainGame();
    }

    public bool IsCountDown()
    {
        return m_timer.CurrentTime <= 4;
    }

    public bool IsGameOver()
    {
        return m_timer.CurrentTime <= 0;
    }

    public void StartGame()
    {
        m_timer.StartTimer();
        m_coinSpawnner.StartCoinSpawn();
    }

    public void FinishGame()
    {
        m_coinSpawnner.StopCoinSpawn();
    }

    public void ResetMainGame()
    {
        m_timer.ResetTimer();
        m_scoreManager.ResetScore();
        m_coinSpawnner.StopCoinSpawn();
    }
}