using UnityEngine;
using UnityEngine.Rendering.PostProcessing; // Post Processing v2の場合

public class resultscene : MonoBehaviour
{
    private bool postProcessEnabled = false;//ぼかし用
    public static bool IsTimeOver { get; private set; } = false;

    void Update()
    {
        if (TimerPlay.IsTimeOver && !postProcessEnabled)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                var postLayer = cam.GetComponent<PostProcessLayer>();
                if (postLayer != null)
                {
                    postLayer.enabled = true;
                    postProcessEnabled = true;
                }
            }
            // リザルト処理もここで呼び出す
        }
    }
}
