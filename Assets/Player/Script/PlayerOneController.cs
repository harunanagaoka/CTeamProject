using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    private Rigidbody rigidBody;

    bool isjumping_P1 = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float h1 = Input.GetAxis("Horizontal_P1");
        float V1 = Input.GetAxis("Vertical_P1");
        rigidBody.AddForce(Vector3.right * h1 * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        rigidBody.AddForce(Vector3.forward * V1 * moveSpeed * Time.deltaTime, ForceMode.Impulse);

        if (!isjumping_P1 && Input.GetButtonDown("Jump_P1"))
        {
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isjumping_P1 = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
        {
            isjumping_P1 = false;
        }
    }
}
