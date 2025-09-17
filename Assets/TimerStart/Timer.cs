using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを使うときに必要

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text TimerText;

    [SerializeField]
    Button startButton; // スタート用ボタン

    // ゲーム開始フラグ（他スクリプトから参照できるようにstaticにする）
    public static bool IsGameStarted { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = ""; // 最初は空にしておく
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStartButtonPressed()
    {
        startButton.interactable = false; // ボタンを無効化
        StartCoroutine(CountdownCoroutine());
        startButton.gameObject.SetActive(false);
    }

    IEnumerator CountdownCoroutine()
    {
        string[] countdownTexts = { "3", "2", "1", "Go!" };
        foreach (var text in countdownTexts)
        {
            TimerText.text = text;
            yield return new WaitForSeconds(1f);
        }

        // 「Go!」表示後2秒待つ
        yield return new WaitForSeconds(2f);

        // UIを非表示にする
        TimerText.gameObject.SetActive(false);

        // ここでゲーム開始
        IsGameStarted = true;
    }
}
