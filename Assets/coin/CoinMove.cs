using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField]
    float velocity = 0;

    Rigidbody rb;

    Vector3 ve = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        ve = new Vector3(velocity, 0, 0);
    }

    void FixedUpdate()
    {
        if(this.transform.parent.transform.position.y < 0.88f && this.transform.parent.transform.position.y > 0.75f)
        {
            rb.linearVelocity = ve;
        }
    }
}
