using System.Collections;
using UnityEngine;
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
    private Loopinterval m_coinSpawnner = null;

    //リザルトシーン
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

                StartCoroutine(ProcessGameStart());
            }

            return;
        }

        if (m_gameScenes.CurrentScene == GameScene.Result)
        {


            return;
        }
    }

    private void LoadTitleScene()
    {
        m_musicManager.OnPlay(MusicManager.MusicName.Title);
    }

    private IEnumerator ProcessGameStart()
    {
        m_SEManager.OnPlayOneShot(SEManager.SoundEffectName.cofirm);
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

    void Debug()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
