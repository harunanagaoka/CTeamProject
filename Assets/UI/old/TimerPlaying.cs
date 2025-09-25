using UnityEngine;

public class TimerPlay : MonoBehaviour
{
    public static bool IsTimeOver { get; private set; } = false;

    [SerializeField]
    private int playTime = 30; // プレイ時間（秒）

    private float currentTime;
    private bool isTimeOver = false;

    void Start()
    {
        currentTime = playTime;
    }

    void Update()
    {
        // ゲームが開始していなければ何もしない
        if (!Timer.IsGameStarted || isTimeOver) return;

        // 残り時間を減らす
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isTimeOver = true;
            // ここで制限時間終了時の処理を記述
            Debug.Log("制限時間終了！");
            IsTimeOver = true;
        }

        // 必要ならUI表示など
        // Debug.Log("残り時間: " + Mathf.CeilToInt(currentTime));
    }
}