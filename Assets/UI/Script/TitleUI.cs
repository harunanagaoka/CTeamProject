using UnityEngine;
using System.Collections;

public class TitleUI : MonoBehaviour
{
    private Canvas titleCanvas = null;

    private UnityEngine.UI.Image startImage = null;

    [SerializeField]
    private float fadeOutDuration = 0f;

    void Awake()
    {
        titleCanvas = GetComponent<Canvas>();
        startImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void SetCanvasVisible(bool visible)
    {
        titleCanvas.enabled = visible;
    }

    public IEnumerator FadeOutImage()
    {
        if (startImage == null) yield break;
        float startAlpha = startImage.color.a;
        float time = 0f;
        Color c = startImage.color;

        while (time < fadeOutDuration)
        {
            time += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(startAlpha, 0f, time / fadeOutDuration);
            startImage.color = c;
            yield return null;
        }

        c.a = 0f;
        startImage.color = c;
    }
}
