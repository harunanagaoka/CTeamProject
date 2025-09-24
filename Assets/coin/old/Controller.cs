using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // ゲーム開始前は物理挙動を止める
        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (Timer.IsGameStarted)
        {
            // ゲーム開始時に物理挙動を有効化
            if (rb != null && rb.isKinematic)
                rb.isKinematic = false;

            // 追加で移動処理をしたい場合はここに記述
            // 例: transform.Translate(Vector3.right * Time.deltaTime * 2f);
        }
    }
}