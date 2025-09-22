using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 追加

public class button_retry : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

/*    [SerializeField] private GameObject resultObjectsParent;*/ // InspectorでResultObjectsをアサイン
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject titleButton;


    void Start()
    {
 //       remainingtime.IsTimeOver = true;
    }

    void Update()
    {




        // --- 以降は既存のボタン処理 ---
        if (remainingtime.IsTimeOver)
        {
                // どちらのボタンも選択されていない場合、両方表示する
                if (retryButton != null && !retryButton.activeSelf)
                    retryButton.SetActive(true);
                if (titleButton != null && !titleButton.activeSelf)
                    titleButton.SetActive(true);

            this.enabled = true;

        }

        if (!remainingtime.IsTimeOver)
        {
            // 制限時間内ならボタンを非表示にする
            if (retryButton != null && retryButton.activeSelf)
                retryButton.SetActive(false);
            if (titleButton != null && titleButton.activeSelf)
                titleButton.SetActive(false);
            this.enabled = false;
        }

        // Enterキーでリトライ
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("リトライが選択されました");
            remainingtime.IsTimeOver = false;


            // リザルト用オブジェクトを全てDestroy
            //if (resultObjectsParent != null)
            //{
            //    //foreach (Transform child in resultObjectsParent.transform)
            //    //{
            //    //    Destroy(child.gameObject);
            //    //}
            //}

            // Timerのカウントダウンを再開
            if (timer != null)
            {
                timer.RestartCountdown();
            }
        }
        // Spaceキーで最初からやり直し（シーンリロード）
        else if (Keyboard.current.spaceKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("最初からやり直します");
            remainingtime.IsTimeOver = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
