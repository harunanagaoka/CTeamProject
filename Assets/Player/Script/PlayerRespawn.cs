using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] private SEManager SEManager;
    [SerializeField] private float fallThreshold = -10f;
    [SerializeField] private float yOffset = 5f;
    [SerializeField] private float slowFallSpeed = -2f;
    [SerializeField] private float stopHeightOffset = 2f;
    [SerializeField] private float stopDuration = 1f;

    private Rigidbody rb;
    public bool isRespawning = false;
    private bool isSlowFalling = false;
    private float stopY;

    private PlayerBlink playerBlink;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition =transform.position ;
        startRotation = transform.rotation;

        // Blinkスクリプトを取得
        playerBlink = GetComponent<PlayerBlink>();
    }

    void Update()
    {
        if (!isRespawning && transform.position.y < fallThreshold)
        {
            StartCoroutine(Respawn());
        }

        if (isSlowFalling)
        {
            rb.linearVelocity = new Vector3(0, slowFallSpeed, 0);

            if (transform.position.y <= stopY)
            {
                StartCoroutine(StopInAir());
            }
        }
    }

    IEnumerator Respawn()
    {
        isRespawning = true;

        rb.isKinematic = true;
        transform.position = startPosition + Vector3.up * yOffset;
        transform.rotation = startRotation;


        // 点滅開始（リスポーン時間と同じ）
        if (playerBlink != null)
        {
            playerBlink.StartBlinking(stopDuration + 1f);
        }

        yield return new WaitForSeconds(0.2f);

        SEManager.OnPlayOneShot(SEManager.SoundEffectName.pl_respawn);

        rb.isKinematic = false;
        isSlowFalling = true;

        stopY = startPosition.y + (yOffset / 2f) - stopHeightOffset;
    }

    IEnumerator StopInAir()
    {
        isSlowFalling = false;

        rb.isKinematic = true;
       // rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(stopDuration);

        rb.isKinematic = false;
        isRespawning = false;
    }

    public bool IsRespawning()
    {
        return isRespawning;
    }
}
