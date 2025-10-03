using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameCoordinator : MonoBehaviour
{
    [SerializeField]
    private MusicManager m_musicManager;

    [SerializeField]
    private SEManager m_SEManager;

    [SerializeField]
    private GameScenes m_gameScenes;

    [SerializeField]
    private PlayerOneController m_player_One;

    [SerializeField]
    private PlayerTwoController m_player_Two;

    [SerializeField]
    private MainGameManager m_mainGameManager;

    //タイトルシーン
    private TitleUI m_titleUI;

    //メインシーン
    private MainGameUI m_mainGameUI;

    private bool m_isCountDown = false;

    //リザルトシーン
    private ResultUI m_resultUI;

    //入力を受け付けるかどうか
    private bool m_canInput = true;

    private void Start()
    {
        m_titleUI = gameObject.GetComponentInChildren<TitleUI>();
        m_mainGameUI = gameObject.GetComponentInChildren<MainGameUI>();
        m_resultUI = gameObject.GetComponentInChildren<ResultUI>();

        if (m_gameScenes.CurrentScene == GameScene.Title)
        {
            Time.timeScale = 0f;
            LoadTitleScene();
        }
    }

    private void Update()
    {
        //メインシーンの時間が一番長いため、メインシーンの処理を一番上に書いています。
        switch (m_gameScenes.CurrentScene)
        {
            case GameScene.Main:

                UpdateMainScene();

                break;
            case GameScene.Title:

                UpdateTitleScene();

                break;
            case GameScene.Result:

                UpdateResultScene();

                break;
        }
    }

    private void UpdateMainScene()
    {
        //3秒前からカウントダウン
        if (m_mainGameManager.IsCountDown() && !m_isCountDown)
        {
            m_isCountDown = true;
            StartCoroutine(CountDown());
        }

        //タイムアップ時の処理、リザルトへ
        if (m_mainGameManager.IsGameOver())
        {
            m_canInput = false;
            TransitionToResult();
            m_gameScenes.ChangeScene(GameScene.Result);
        }
    }

    private void UpdateTitleScene()
    {
        if (!m_canInput)
        {
            return;
        }

        //ボタンでメインシーンへ
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            m_canInput = false;
            m_musicManager.OnStop();
            m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.cofirm);

            StartCoroutine(ProcessGameStart());
        }
    }

    private void UpdateResultScene()
    {
        if (!m_canInput)
        {
            return;
        }

        //ボタンでリトライ
        if (Keyboard.current.enterKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            ResetGame();
            m_resultUI.ResetUI();
            m_resultUI.SetCanvasVisible(false);

            StartCoroutine(ProcessGameStart());
        }

        //ボタンでタイトルへ
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            m_resultUI.ResetUI();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void LoadTitleScene()
    {
        m_musicManager.OnPlay(MusicManager.MusicName.Title);
        m_mainGameUI.SetCanvasVisible(false);
        m_resultUI.SetCanvasVisible(false);
        m_titleUI.SetCanvasVisible(true);
    }

    private IEnumerator ProcessGameStart()
    {
        m_mainGameManager.ResetMainGame();
        m_mainGameUI.SetCanvasVisible(true);

        yield return StartCoroutine(m_titleUI.FadeOutImage());

        m_titleUI.SetCanvasVisible(false);

        yield return StartCoroutine(CountDown());

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.whistle);
        m_musicManager.OnPlay(MusicManager.MusicName.Main);
        m_mainGameManager.StartGame();
        Time.timeScale = 1.0f;
        m_gameScenes.ChangeScene(GameScene.Main);
        m_canInput = true;
    }

    private void TransitionToResult()
    {
        Time.timeScale = 0f;
        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.whistle);
        m_mainGameManager.FinishGame();
        m_mainGameUI.SetCanvasVisible(false);
        m_resultUI.SetCanvasVisible(true);
        
        StartCoroutine(ProcessResult());

    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSecondsRealtime(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSecondsRealtime(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSecondsRealtime(1f);
    }

    private IEnumerator ProcessResult()
    {
        m_musicManager.OnplayResultBGM();

        yield return new WaitForSecondsRealtime(5f);

        m_musicManager.OnPlay(MusicManager.MusicName.ResultLoop);
        m_resultUI.AppearResultImage();

        yield return new WaitForSecondsRealtime(1f);

        m_resultUI.AppearBottuns();
        m_canInput = true;
    }

    private void ResetGame()
    {
        //タイマーとスコアリセット
        m_isCountDown = false;
        m_musicManager.OnStop();
        m_player_One.ResetPosition();
        m_player_Two.ResetPosition();
    }
}
