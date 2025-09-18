using UnityEngine;
using UnityEngine.Rendering.PostProcessing; // Post Processing v2‚Ìê‡

public class resultscene : MonoBehaviour
{
    private bool postProcessEnabled = false;

    void Update()
    {
        if (remainingtime .IsTimeOver && !postProcessEnabled)
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
            // ƒŠƒUƒ‹ƒgˆ—‚à‚±‚±‚ÅŒÄ‚Ño‚·
        }
    }
}
