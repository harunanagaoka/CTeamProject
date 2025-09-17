using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを使うときに必要

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text TimerText;

    [SerializeField]
    Button startButton; // 使わなくてもOKですがInspectorから外しても良い

    // ゲーム開始フラグ（他スクリプトから参照できるようにstaticにする）
    public static bool IsGameStarted { get; private set; } = false;

    private bool isCountdownStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = " "; // 最初は空にしておく
        startButton.onClick.AddListener(OnStartButtonPressed);
        
    }

    void OnStartButtonPressed()
    {
        if (isCountdownStarted) return;
        isCountdownStarted = true;
        startButton.interactable = false;
        Destroy(startButton.gameObject); // ボタンを消す
        StartCoroutine(CountdownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // まだカウントダウンが始まっていない場合のみBボタン入力を受け付ける
        if (!isCountdownStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            isCountdownStarted = true;
            if (startButton != null)
            {
                startButton.interactable = false;
                Destroy(startButton.gameObject); // Space/Bボタンでもボタンを消す
            }
            StartCoroutine(CountdownCoroutine());
        }
    }

    IEnumerator CountdownCoroutine()
    {
        string[] countdownTexts = { "3", "2", "1", "Go!" };
        foreach (var text in countdownTexts)
        {
            TimerText.text = text;
            yield return new WaitForSeconds(1f);
        }

        // 「Go!」表示後1秒待つ
        yield return new WaitForSeconds(1f);

        // UIを非表示にする
        TimerText.gameObject.SetActive(false);

        // ここでゲーム開始
        IsGameStarted = true;
    }

    public void RestartCountdown()//採用戦を押した場合にカウントダウンのところまで戻ってくる用の関数
    {
        StopAllCoroutines();
        IsGameStarted = false;
        isCountdownStarted = false;
        TimerText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }
}
