using UnityEngine;
using UnityEngine.UI;

public class remainingtime : MonoBehaviour
{
    public static bool IsTimeOver { get;  set; } = false;
    public static int PlayTime { get; private set; }
    public static float CurrentTime { get; private set; }

    [SerializeField] private int playTime = 30; // プレイ時間（秒）
    [SerializeField] private Text remaining_timeText;
    [SerializeField] private Text player1ScoreText; // 追加
    [SerializeField] private Text player2ScoreText; // 追加
    [SerializeField] private Font customFont;
    [SerializeField] private button_retry button_Retry = null;

    private float currentTime;

    void Start()
    {
        currentTime = playTime;

        if (remaining_timeText != null && customFont != null)
        {
            remaining_timeText.font = customFont;
        }
        if (player1ScoreText != null && customFont != null)
        {
            player1ScoreText.font = customFont;
        }
        if (player2ScoreText != null && customFont != null)
        {
            player2ScoreText.font = customFont;
        }
    }

    void Update()
    {
        Debug.Log("残り時間" + currentTime);

        // ゲームが開始していなければ何もしない
        if (!Timer.IsGameStarted || IsTimeOver) return;

        // 残り時間を減らす
        currentTime -= Time.deltaTime;
        CurrentTime = currentTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            IsTimeOver = true;
            Debug.Log("制限時間終了！");
            if (button_Retry != null)
            {
                button_Retry.enabled = true;
            }
        }

        // 残り時間を整数で表示
        int displayTime = Mathf.CeilToInt(currentTime);
        if (remaining_timeText != null)
        {
            remaining_timeText.text = displayTime.ToString();
        }

        // スコア表示を更新
        if (player1ScoreText != null)
        {
            player1ScoreText.text = "P1: " + Score1.player1Score.ToString();
        }
        if (player2ScoreText != null)
        {
            player2ScoreText.text = "P2: " + Score1.player2Score.ToString();
        }
    }

    public void ResetTime()
    {
        currentTime = playTime;
    }
}
