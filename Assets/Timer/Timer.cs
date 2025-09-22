using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private MusicManager musicManager;

    [SerializeField]
    Text TimerText;

    [SerializeField]
    private remainingtime src;

    [SerializeField]
    private Image timerImage; // ← Imageに変更

    public static bool IsGameStarted { get; private set; } = false;

    private bool isCountdownStarted = false;

    private void Awake()
    {
        UI.isShown = false;
    }

    void Start()
    {
        musicManager.OnPlay(MusicManager.MusicName.Title);
        TimerText.text = " ";
        if (timerImage != null)
        {
            var c = timerImage.color;
            c.a = 1f;
            timerImage.color = c;
        }
    }

    void Update()
    {
        if (!isCountdownStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            musicManager.OnStop();
            isCountdownStarted = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    IEnumerator CountdownCoroutine()//一度目のスタート用
    {
        // フェードアウト開始
        yield return StartCoroutine(FadeOutImage(timerImage, 1.0f));

        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(1f);

        TimerText.gameObject.SetActive(false);

        IsGameStarted = true;

        musicManager.OnPlay(MusicManager.MusicName.Main);
    }

    IEnumerator ReCountdownCoroutine()//リトライ用
    {
        // フェードアウト開始

        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(1f);

        TimerText.gameObject.SetActive(false);

        IsGameStarted = true;
    }


    IEnumerator FadeOutImage(Image image, float duration)
    {
        if (image == null) yield break;
        float startAlpha = image.color.a;
        float time = 0f;
        Color c = image.color;
        while (time < duration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, 0f, time / duration);
            image.color = c;
            yield return null;
        }
        c.a = 0f;
        image.color = c;
    }

    public void RestartCountdown()
    {
        src.ResetTime();

        StopAllCoroutines();
        UI.isShown = false;
        IsGameStarted = false;
        isCountdownStarted = false;
        TimerText.gameObject.SetActive(true);

        if (timerImage != null)
        {
            var c = timerImage.color;
            c.a = 1f;
            timerImage.color = c;
        }

        StartCoroutine(ReCountdownCoroutine()); // ← ここを追加

        Debug.Log("リトライが選択されました");
    }

    public void FinishMainBGM()
    {
        musicManager.OnStop();
    }
}
