using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 0;

    private Vector3 m_moveDirection = Vector3.zero;

    private Vector3 m_moveVector = Vector3.zero;

    private string m_tagName = "Player";

    void Start()
    {
        //m_moveVector = m_moveDirection.normalized * m_moveSpeed;
        m_moveVector = this.transform.forward * m_moveSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
         if(collision.collider.tag == m_tagName)
         {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            rb.AddForce(m_moveVector,ForceMode.Force);
            } 
    }

}
