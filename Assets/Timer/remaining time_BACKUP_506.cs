using UnityEngine;

public class remainingtime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        
=======
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
>>>>>>> d8598b4d43ff204ea2fb0d1e8f33c18ad0dedb79
    }
}
