using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private MusicManager m_musicManager;

    [SerializeField]
    private SEManager m_SEManager;

    [SerializeField]
    private GameScenes m_gameScenes = null;

    //タイトルシーン
    [SerializeField]
    private Canvas m_titleCanvas = null;

    [SerializeField]
    private UnityEngine.UI.Image m_startImage = null;

    [SerializeField]
    private float m_fadeOutDuration = 0f;

    //メインシーン
    [SerializeField]
    private Canvas m_mainCanvas = null;

    private MainGameTimer m_mainGameTimer = null;

    [SerializeField]
    private ScoreManager m_scoreManager = null;

    [SerializeField]
    private Loopinterval m_coinSpawnner = null;

    private bool m_isCountDown = false;

    //リザルトシーン
    [SerializeField]
    private ResultUI m_resultUI = null;

    [SerializeField]
    private Canvas m_resultCanvas = null;

    //入力を受け付けるかどうか
    private bool m_canInput = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_mainGameTimer = gameObject.GetComponent<MainGameTimer>();

        if (m_gameScenes.CurrentScene == GameScene.Title)
        {
            LoadTitleScene();
        }
    }

    private void Update()
    {
        Debug();

        //メインシーンの時間が一番長いため、メインシーンの処理を一番上に書いています。
        if (m_gameScenes.CurrentScene == GameScene.Main)
        {
            //タイマー再生、タイムアップでリザルトへ
            if(m_mainGameTimer.CurrentTime <= 4 && !m_isCountDown)
            {
                m_isCountDown = true;
                StartCoroutine(CountDown());
            }

            if (m_mainGameTimer.CurrentTime <= 0)
            {
                m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.whistle);
                m_coinSpawnner.StopCoinSpawn();
                m_mainCanvas.enabled = false;
                m_resultCanvas.enabled = true;
                m_canInput = false;

                StartCoroutine(ProcessResult());

                m_gameScenes.ChangeScene(GameScene.Result);
            }

            return;
        }

        if (m_gameScenes.CurrentScene == GameScene.Title)
        {
            if (!m_canInput)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                m_canInput = false;

                m_musicManager.OnStop();

                m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.cofirm);

                StartCoroutine(ProcessGameStart());
            }

            return;
        }

        if (m_gameScenes.CurrentScene == GameScene.Result)
        {
            if (!m_canInput)
            {
                return;
            }

            if (Keyboard.current.enterKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                //リトライ
                ResetMainGame();
                m_resultUI.ResetUI();
                m_resultCanvas.enabled = false;

                StartCoroutine(ProcessGameStart());

                //UIリセット
            }

            if( Keyboard.current.spaceKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton0)){
                //リセット
                m_resultUI.ResetUI();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }


                return;
        }
    }

    private void LoadTitleScene()
    {
        m_musicManager.OnPlay(MusicManager.MusicName.Title);
        m_mainCanvas.enabled = false;
        m_resultCanvas.enabled = false;
        m_titleCanvas.enabled = true; ;
    }

    private IEnumerator ProcessGameStart()
    {
        
        m_mainCanvas.enabled = true;
        m_mainGameTimer.ResetTimer();

        yield return StartCoroutine(FadeOutImage());

        m_titleCanvas.enabled = false;

        yield return StartCoroutine(CountDown());

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.whistle);
        m_musicManager.OnPlay(MusicManager.MusicName.Main);
        m_mainGameTimer.StartTimer();
        m_coinSpawnner.StartCoinSpawn();
        m_gameScenes.ChangeScene(GameScene.Main);

        m_canInput = true;
    }
    private IEnumerator FadeOutImage()
    {
        if (m_startImage == null) yield break;
        float startAlpha = m_startImage.color.a;
        float time = 0f;
        Color c = m_startImage.color;
        while (time < m_fadeOutDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, 0f, time / m_fadeOutDuration);
            m_startImage.color = c;
            yield return null;
        }
        c.a = 0f;
        m_startImage.color = c;
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSeconds(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSeconds(1f);

        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.CountDown);

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator ProcessResult()
    {
        m_musicManager.OnplayResultBGM();

        yield return new WaitForSeconds(5f);

        m_musicManager.OnPlay(MusicManager.MusicName.ResultLoop);

        m_resultUI.AppearResultImage();

        yield return new WaitForSeconds(1f);

        m_resultUI.AppearBottuns();

        m_canInput = true;
    }

    private void ResetMainGame()
    {
        //タイマーとスコアリセット
        m_isCountDown = false;
        m_musicManager.OnStop();
        m_mainGameTimer.ResetTimer();
        m_scoreManager.ResetScore();
    }



    void Debug()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
