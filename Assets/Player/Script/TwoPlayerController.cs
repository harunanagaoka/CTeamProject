using UnityEngine;

public class TwoPlayerController : MonoBehaviour
{

    public Transform player2;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb1;
    private Rigidbody rb2;

    bool isjumping_P1 = false;
    bool isjumping_P2 = false;

    void Start()
    {

        rb2 = player2.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- Player2 ---
        float h2 = Input.GetAxis("Horizontal_P2");
        player2.Translate(Vector3.right * h2 * moveSpeed * Time.deltaTime);
        if (!isjumping_P2 && Input.GetButtonDown("Jump_P2"))
        {
            rb2.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isjumping_P2 |= true;
        }
    }
}
