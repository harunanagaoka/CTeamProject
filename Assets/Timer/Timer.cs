using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを使うときに必要

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text TimerText;

    [SerializeField]
    private remainingtime src; // 追加

    [SerializeField]
    private CanvasGroup timerCanvasGroup; // 追加

    // ゲーム開始フラグ（他スクリプトから参照できるようにstaticにする）
    public static bool IsGameStarted { get; private set; } = false;

    private bool isCountdownStarted = false;

    // Start is called before the first frame update

    private void Awake()
    {
        UI.isShown = false;
    }
    void Start()
    {
        TimerText.text = " "; // 最初は空にしておく
        timerCanvasGroup.alpha = 1f; // 念のため初期化
    }

    // Update is called once per frame
    void Update()
    {
        // まだカウントダウンが始まっていない場合のみBボタン入力を受け付ける
        if (!isCountdownStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            isCountdownStarted = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    IEnumerator CountdownCoroutine()
    {
        // フェードアウト開始
        yield return StartCoroutine(FadeOutCanvasGroup(timerCanvasGroup, 1.0f));

        yield return new WaitForSeconds(3f);

        // 「Go!」表示後1秒待つ
        yield return new WaitForSeconds(1f);

        // UIを非表示にする（完全に消したい場合）
        TimerText.gameObject.SetActive(false);

        // ここでゲーム開始
        IsGameStarted = true;
    }

    IEnumerator FadeOutCanvasGroup(CanvasGroup group, float duration)
    {
        float startAlpha = group.alpha;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            yield return null;
        }
        group.alpha = 0f;
    }

    public void RestartCountdown()//再戦を押した場合にカウントダウンのところまで戻ってくる用の関数
    {
        src.ResetTime();

        StopAllCoroutines();
        UI.isShown = false; // UIの表示をリセット
        IsGameStarted = false;
        isCountdownStarted = false;
        TimerText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());

        Debug.Log("リトライが選択されました");
    }
}
