using UnityEngine;
using UnityEngine.UI;

public class remainingtime : MonoBehaviour
{
    public static bool IsTimeOver { get;  set; } = false;
    public static int PlayTime { get; private set; }
    public static float CurrentTime { get; private set; }

    [SerializeField] private int playTime = 30; // プレイ時間（秒）
    [SerializeField] private Text remaining_timeText;
    //[SerializeField] private Animation end;
    [SerializeField] private Font customFont;
    [SerializeField] private GameObject buttonResult;

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

        Debug.Log("残り時間" + currentTime);


        // ゲームが開始していなければ何もしない
        if (!Timer.IsGameStarted || isTimeOver) return;

        // 残り時間を減らす
        currentTime -= Time.deltaTime;
        CurrentTime = currentTime;

        if (currentTime <= 0f)
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            
        //if (end != null)
        //    {
        //        end.Play();
        //    }
            isTimeOver = true;
            Debug.Log("制限時間終了！");
                // 制限時間終了時にbuttonResultをアクティブにする
                if (buttonResult != null)
                {
                    buttonResult.SetActive(remainingtime.IsTimeOver);
                }

            }

        // 残り時間を整数で表示
        int displayTime = Mathf.CeilToInt(currentTime);
        if (remaining_timeText != null)
        {
            remaining_timeText.text = displayTime.ToString();
        }
    }
    //remainingTime内で以下の関数を宣言する
    public void ResetTime()
    {
        currentTime = playTime;
    }

    public void debug()
    {
        Debug.Log(currentTime);
    }

}
