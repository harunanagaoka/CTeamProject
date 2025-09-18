using UnityEngine;
using System.Collections;

public class PlayerBlink : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;   // 点滅させたい子のRenderer
    [SerializeField] private float blinkInterval = 0.2f;

    private Color originalColor;
    private Coroutine blinkCoroutine;

    void Start()
    {
        if (targetRenderer != null)
        {
            // マテリアルをインスタンス化して他のプレハブに影響させない
            targetRenderer.material = new Material(targetRenderer.material);
            originalColor = targetRenderer.material.GetColor("_BaseColor");
        }
        else
        {
            Debug.LogWarning("TransparentBlink: targetRenderer が未設定です。");
        }
    }

    public void StartBlinking(float duration)
    {
        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkRoutine(duration));
    }

    private IEnumerator BlinkRoutine(float duration)
    {
        if (targetRenderer == null) yield break;

        float elapsed = 0f;
        bool isTransparent = false;

        while (elapsed < duration)
        {
            Color c = originalColor;
            c.a = isTransparent ? 0.5f : originalColor.a; // 透明 ⇄ 元のアルファ
            targetRenderer.material.SetColor("_BaseColor", c);

            isTransparent = !isTransparent;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        // 点滅終了後は元の色に戻す
        targetRenderer.material.SetColor("_BaseColor", originalColor);
    }
}
