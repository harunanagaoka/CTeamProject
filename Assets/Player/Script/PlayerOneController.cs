using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerOneController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float groundMoveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float fallMultiplier = 3.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Air Control Settings")]
    [SerializeField, Range(0f, 1f)] private float airControl = 0.2f; // 空中での操作割合
    [SerializeField] private float airMoveSpeed = 2f;                // 空中での移動速度

    private Rigidbody rb;
    private bool isJumping = false;

    // 入力
    private const string HORIZONTAL = "Horizontal_P1";
    private const string VERTICAL = "Vertical_P1";
    private const string JUMP = "Jump_P1";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 inputDir = new Vector3(Input.GetAxis(HORIZONTAL), 0, Input.GetAxis(VERTICAL));

        if (inputDir.sqrMagnitude > 0.001f)
        {
            Vector3 move;

            if (!isJumping)
            {
                // --- 地上：そのまま自由移動 ---
                move = inputDir.normalized * groundMoveSpeed;
            }
            else
            {
                // --- 空中：弱めに制御（慣性＋入力少しだけ） ---
                Vector3 currentXZ = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                Vector3 desiredXZ = inputDir.normalized * airMoveSpeed;

                // 慣性に入力をちょっとだけ混ぜる
                Vector3 blended = Vector3.Lerp(currentXZ, desiredXZ, airControl);
                move = blended;
            }

            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

            // --- 回転処理 ---
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(inputDir.x, 0, inputDir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }
        else if (!isJumping)
        {
            // 地上で入力なし → ピタッと止まる
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        // --- ジャンプ挙動補正 ---
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Physics.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton(JUMP))
        {
            rb.AddForce(Physics.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if (!isJumping && Input.GetButtonDown(JUMP))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))//collision.gameObject.CompareTag("Ground") || 
            isJumping = false;
    }
}
