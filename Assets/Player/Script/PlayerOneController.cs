using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerOneController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;      // 地上/空中共通
    [SerializeField] private float rotationSpeed = 8f;  // 滑らかな回転

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float fallMultiplier = 2f;


    private string horizontalAxis = "Horizontal_P1";
    private string verticalAxis = "Vertical_P1";
    private string jumpButton = "Jump_P1";


    private Rigidbody rb;
    private bool isJumping = false;
    private float lastY;
    private bool isFalling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastY = rb.position.y;
    }

    void FixedUpdate()
    {
        // 上昇/落下判定
        float currentY = rb.position.y;
        isFalling = currentY < lastY;
        lastY = currentY;

        // 移動入力
        Vector3 inputDir = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
        if (inputDir.sqrMagnitude > 0.001f)
        {
            // 回転
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);

            // 移動（ジャンプ中もフル速度）
            rb.MovePosition(rb.position + inputDir.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        // 落下加速
        if (isFalling)
        {
            rb.AddForce(Physics.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (!isJumping && Input.GetButtonDown(jumpButton))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isJumping = false;
    }
}
