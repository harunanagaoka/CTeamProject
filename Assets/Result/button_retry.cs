using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 追加

public class button_retry : MonoBehaviour
{
    private Timer timer;
    private int selectnum = 0;

    [SerializeField] private GameObject resultObjectsParent; // InspectorでResultObjectsをアサイン

    void Start()
    {
        timer = Object.FindFirstObjectByType<Timer>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (selectnum == 0)
        {
            // ↓キーで下に移動
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                selectnum = 1;
            }

            // Enterキーでリトライ
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                Debug.Log("リトライが選択されました");

                // リザルト用オブジェクトを全てDestroy
                if (resultObjectsParent != null)
                {
                    foreach (Transform child in resultObjectsParent.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                // Timerのカウントダウンを再開
                if (timer != null)
                {
                    timer.RestartCountdown();
                }
            }
        }
        else
        {
            // ↑キーで上に移動
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                selectnum = 0;
            }

            // Enterキーでアプリ終了
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                Debug.Log("終了処理が呼ばれました");
                Application.Quit();
            }
        }
    }
}
