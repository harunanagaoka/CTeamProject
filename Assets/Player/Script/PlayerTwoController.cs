using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTwoController : MonoBehaviour
{
    [SerializeField]
    private SEManager SEManager = null;

    [Header("Movement Settings")]
    [SerializeField] private float groundMoveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float fallMultiplier = 3.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Air Control Settings")]
    [SerializeField, Range(0f, 1f)] private float airControl = 0.2f;
    [SerializeField] private float airMoveSpeed = 2f;

    [Header("Jump Options")]
    [SerializeField] private bool ignoreConveyorOnJump = true;
    // true = ベルト速度を無視して「真上 or 入力方向ジャンプ」
    // false = ベルト速度を含めてジャンプ

    private Rigidbody rb;
    private bool isJumping = false;

    private const string HORIZONTAL = "Horizontal_P2";
    private const string VERTICAL = "Vertical_P2";
    private const string JUMP = "Jump_P2";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var respawn = GetComponent<PlayerRespawn>();
        if (respawn != null && respawn.IsRespawning())
            return;

        float h = Input.GetAxis(HORIZONTAL);
        float v = Input.GetAxis(VERTICAL);
        Vector3 inputDir = new Vector3(h, 0, v);

        Vector3 move = Vector3.zero;

        if (inputDir.sqrMagnitude > 0.001f)
        {
            if (!isJumping)
            {
                move = inputDir.normalized * groundMoveSpeed;
            }
            else
            {
                Vector3 currentXZ = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                Vector3 desiredXZ = inputDir.normalized * airMoveSpeed;
                Vector3 blended = Vector3.Lerp(currentXZ, desiredXZ, airControl);
                move = blended;
            }

            // 見た目の回転
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(inputDir.x, 0, inputDir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }

        // 足元がコンベアーならその速度を加算
        Vector3 conveyorVel = Vector3.zero;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f))
        {
            if (hit.collider.CompareTag("BeltConveyor"))
            {
                ConveyorMove belt = hit.collider.GetComponent<ConveyorMove>();
                if (belt != null)
                    conveyorVel = belt.GetConveyorVelocity();
            }
        }

        // 最終速度 = プレイヤー速度 + コンベアー速度
        rb.linearVelocity = new Vector3(
            move.x + conveyorVel.x,
            rb.linearVelocity.y,
            move.z + conveyorVel.z
        );

        // ジャンプ挙動の補正
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Physics.gravity * (fallMultiplier - 1), ForceMode.Force);
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton(JUMP))
        {
            rb.AddForce(Physics.gravity * (lowJumpMultiplier - 1), ForceMode.Force);
        }
    }

    void Update()
    {
        if (!isJumping && Input.GetButtonDown(JUMP))
        {
            // 現在の速度を取得
            Vector3 vel = rb.linearVelocity;

            // 横方向は「入力中なら入力ベクトル」「入力がなければ現状の速度」を使う
            float h = Input.GetAxis(HORIZONTAL);
            float v = Input.GetAxis(VERTICAL);
            Vector3 inputDir = new Vector3(h, 0, v);

            if (inputDir.sqrMagnitude > 0.001f)
            {
                vel.x = inputDir.normalized.x * groundMoveSpeed;
                vel.z = inputDir.normalized.z * groundMoveSpeed;
            }
            // 入力がないときは vel.x, vel.z はそのまま保持（慣性を残す）

            // Y方向だけリセット
            vel.y = 0f;
            rb.linearVelocity = vel;

            // 上方向にジャンプ力を加える
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;

            SEManager.OnPlayOneShot(SEManager.SoundEffectName.pl_Jamp);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isJumping = false;
    }
}
