using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 startPosition;        // 初期位置
    public float fallThreshold = -10f;   // 落下判定の高さ
    public float yOffset = 5f;           // 初期位置より上にリスポーン
    public float slowFallSpeed = -2f;    // ゆっくり落下の速度
    public float stopHeightOffset = 2f;  // 初期位置から止まる高さの差
    public float stopDuration = 1f;      // 空中で止まる時間

    private Rigidbody rb;
    private bool isRespawning = false;
    private bool isSlowFalling = false;
    private float stopY;                 // 停止する高さ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = startPosition;
    }

    void Update()
    {
        if (!isRespawning && transform.position.y < fallThreshold)
        {
            StartCoroutine(Respawn());
        }

        // ゆっくり落下中
        if (isSlowFalling)
        {
            rb.linearVelocity = new Vector3(0, slowFallSpeed, 0);

            // 停止高さに到達したら止まる
            if (transform.position.y <= stopY)
            {
                StartCoroutine(StopInAir());
            }
        }
    }

    IEnumerator Respawn()
    {
        isRespawning = true;

        // 動きを止めてリスポーン位置に移動
        rb.isKinematic = true;
        transform.position = startPosition + Vector3.up * yOffset;

        yield return new WaitForSeconds(0.2f); // 少し待ってから落下開始

        // ゆっくり落下開始
        rb.isKinematic = false;
        isSlowFalling = true;

        // 停止する高さを設定
        stopY = startPosition.y + (yOffset / 2f) - stopHeightOffset;
    }

    IEnumerator StopInAir()
    {
        isSlowFalling = false;

        // 空中でピタッと停止
        rb.isKinematic = true;
       // rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(stopDuration);

        // 物理再開（通常の落下）
        rb.isKinematic = false;
        isRespawning = false;
    }
}
