using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 0;

    private Vector3 m_moveVector = Vector3.zero;

    private string m_tagName_one = "Player1";

    private string m_tagName_two = "Player2";

    void Start()
    {
        m_moveVector = this.transform.forward * m_moveSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == m_tagName_one || collision.collider.tag == m_tagName_two)
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            rb.AddForce(m_moveVector, ForceMode.Acceleration);
        }
    }
}
