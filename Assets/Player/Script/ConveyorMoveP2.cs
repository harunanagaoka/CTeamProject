using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConveyorMoveP2 : MonoBehaviour
{
    [SerializeField] private Vector3 conveyorDirection = Vector3.right;
    [SerializeField] private float conveyorSpeed = 3f;

    private Rigidbody rb;
    private bool isGrounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isGrounded) return;

        Vector3 vel = rb.linearVelocity;
        Vector3 conveyorVel = conveyorDirection.normalized * conveyorSpeed;

        float h = Input.GetAxis("Horizontal_P2");
        float v = Input.GetAxis("Vertical_P2");

        // PlayerOneController の groundMoveSpeed と揃える
        Vector3 inputVel = new Vector3(h, 0, v).normalized * 5f;

        // ★ 入力があってもベルト速度を必ず加算
        rb.linearVelocity = new Vector3(
            conveyorVel.x + inputVel.x,
            vel.y,
            conveyorVel.z + inputVel.z
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isGrounded = true;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isGrounded = false;
    }

    // ベルト速度を取得する用
    public Vector3 GetConveyorVelocity()
    {
        return conveyorDirection.normalized * conveyorSpeed;
    }

}
