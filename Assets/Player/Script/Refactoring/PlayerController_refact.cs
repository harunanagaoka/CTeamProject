using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_refact : MonoBehaviour
{
    [SerializeField] private SEManager seManager;

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

    //[Header("Jump Options")]　/*使われていなかったためコメントアウトしました。*/
    //[SerializeField] private bool ignoreConveyorOnJump = true;
    //// true = ベルト速度を無視して「真上 or 入力方向ジャンプ」
    //// false = ベルト速度を含めてジャンプ

    private Rigidbody _rigidbody;/*　※　命名の変更*/
    private bool isJumping = false;
    private bool isJumpRequested = false;/*　※　ジャンプ入力と処理を分けるため追加*/
    private PlayerRespawn playerRespawn;/*　※　GetComponentを一回にするため追加*/
    private float inputEpsilon = 0.001f;
    private const string conveyorTagName = "BeltConveyor";

    //ゲームリセット用
    private Vector3 initPos = Vector3.zero;
    private Quaternion initRotation = Quaternion.identity;

    private const string HORIZONTAL = "Horizontal_P1";
    private const string VERTICAL = "Vertical_P1";
    private const string JUMP = "Jump_P1";

    /*　※　入力受け取りをUpdateに一本化するため追加*/
    private Vector3 inputDir = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerRespawn = GetComponent<PlayerRespawn>();
        initPos = transform.position;
        initRotation = transform.rotation;
    }

    private void Update()/*　※入力の受け取り　*/
    {
        //リスポーン中の処理
        if (playerRespawn != null && playerRespawn.IsRespawning())
        {
            inputDir = Vector3.zero;
            return;
        }

        //移動入力受け取り
        inputDir.x = Input.GetAxis(HORIZONTAL);
        inputDir.z = Input.GetAxis(VERTICAL);

        //ジャンプ入力受け取り
        if (!isJumping && Input.GetButtonDown(JUMP))
        {
            seManager.OnPlayOneShot(SEManager.SoundEffectName.pl_Jamp);
            isJumpRequested = true;
        }
    }

    private void FixedUpdate()/*　※RIgidbodyの操作　*/
    {
        //リスポーン中は動かせない
        if (playerRespawn != null && playerRespawn.IsRespawning())
        {
            return;
        }

        //ジャンプ実行処理
        if (TryExecuteJump())
        {
            isJumping = true;
        }

        isJumpRequested = false;

        //移動処理
        Vector3 move = CalculateMoveVelocity();

        if (inputDir.sqrMagnitude > inputEpsilon)
        {
            // 見た目の回転
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(inputDir.x, 0, inputDir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }

        // 足元がコンベアーならその速度を加算
        Vector3 conveyorVel = GetConveyorVelocityAtPosition();

        // 最終速度 = プレイヤー速度 + コンベアー速度
        _rigidbody.linearVelocity = new Vector3(
            move.x + conveyorVel.x,
            _rigidbody.linearVelocity.y,
            move.z + conveyorVel.z
        );

        // ジャンプ挙動の補正　落下中
        if (_rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.AddForce(Physics.gravity * (fallMultiplier - 1), ForceMode.Force);
        }

        //旧ジャンプ挙動　小ジャンプ
        //else if (rb.linearVelocity.y > 0 && !Input.GetButton(JUMP)) 
        //{
        //    rb.AddForce(Physics.gravity * (lowJumpMultiplier - 1), ForceMode.Force);
        //}
    }

    private bool TryExecuteJump()
    {
        if (isJumpRequested && !isJumping)//ジャンプ中でないかつジャンプ入力があったら
        {
            Vector3 vel = _rigidbody.linearVelocity;

            if (inputDir.sqrMagnitude > inputEpsilon)
            { // スティック入力がないときは vel.x, vel.z はそのまま保持（慣性を残す）
                vel.x = inputDir.normalized.x * groundMoveSpeed;
                vel.z = inputDir.normalized.z * groundMoveSpeed;
            }

            // Y方向だけリセット
            vel.y = 0f;
            _rigidbody.linearVelocity = vel;

            // 上方向にジャンプ力を加える
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            return true;
        }

        return false;
    }

    private Vector3 CalculateMoveVelocity()
    {
         // 移動入力がない場合
        if (inputDir.sqrMagnitude <= inputEpsilon)
        {
            return Vector3.zero;
        }

        // 地上での移動
        if (!isJumping)
        {
            return inputDir.normalized * groundMoveSpeed;
        }

        // 空中での移動（慣性が働く）
        Vector3 currentXZ = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        Vector3 desiredXZ = inputDir.normalized * airMoveSpeed;
        return Vector3.Lerp(currentXZ, desiredXZ, airControl);
    }

    private Vector3 GetConveyorVelocityAtPosition()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(conveyorTagName))
            {
                ConveyorMove belt = hit.collider.GetComponent<ConveyorMove>();
                if (belt != null)
                {
                    return belt.GetConveyorVelocity();
                }
            }
        }

        return Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
        {
            isJumping = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = initPos;
        transform.rotation = initRotation;
    }
}
