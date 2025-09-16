using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTwoController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpForce = 5f;

    private Rigidbody rigidBody;

    bool isjumping_P2 = false;
   
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h2 = Input.GetAxis("Horizontal_P2");
        float V2 = Input.GetAxis("Vertical_P2");
        rigidBody.AddForce(Vector3.right * h2 * moveSpeed * Time.deltaTime,ForceMode.Impulse);
        rigidBody.AddForce(Vector3.forward * V2 * moveSpeed * Time.deltaTime, ForceMode.Impulse);

        if (!isjumping_P2 && Input.GetButtonDown("Jump_P2"))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
             isjumping_P2 = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
        {
            isjumping_P2 = false;
        }
    }
}
