using UnityEngine;
using UnityEngine.UI;

public class remainingtime : MonoBehaviour
{
    public static bool IsTimeOver { get; private set; } = false;
    public static int PlayTime { get; private set; }
    public static float CurrentTime { get; private set; }

    [SerializeField] private int playTime = 30; // プレイ時間（秒）
    [SerializeField] private Text remaining_timeText;
    [SerializeField] private Font customFont;

    private float currentTime;
    private bool isTimeOver = false;

    void Start()
    {
        currentTime = playTime;
        PlayTime = playTime;
        CurrentTime = playTime;

        if (remaining_timeText != null && customFont != null)
        {
            remaining_timeText.font = customFont;
        }
    }

    void Update()
    {
        // ゲームが開始していなければ何もしない
        if (!Timer.IsGameStarted || isTimeOver) return;

        // 残り時間を減らす
        currentTime -= Time.deltaTime;
        CurrentTime = currentTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isTimeOver = true;
            Debug.Log("制限時間終了！");
            IsTimeOver = true;
        }

        // 残り時間を整数で表示
        int displayTime = Mathf.CeilToInt(currentTime);
        if (remaining_timeText != null)
        {
            remaining_timeText.text = displayTime.ToString();
        }
    }
}
