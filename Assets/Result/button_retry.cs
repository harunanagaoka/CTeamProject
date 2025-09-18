using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 追加

public class button_retry : MonoBehaviour
{
    private Timer timer;

    [SerializeField] private GameObject resultObjectsParent; // InspectorでResultObjectsをアサイン

    void Start()
    {
        timer = Object.FindFirstObjectByType<Timer>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

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
                    timer.RestartCountdown(); // コルーチンでカウントダウンを再スタート
                }
            }
        

            // Enterキーで最初からやり直し（シーンリロード）
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                
                Debug.Log("最初からやり直します");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
        
    }
}
