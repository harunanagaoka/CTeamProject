using UnityEngine;

public class Move : MonoBehaviour
{
    //[SerializeField]
    //private float rotationSpeed = 180f;

    [SerializeField]
    private float gravityY = -9.81f; // Inspectorで重力の強さを調整可能

    private Rigidbody rb;


    void Awake()
    {
    }

    void Start()
    {
        // Rigidbodyを取得（なければ追加）
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // 重力を有効にする
        rb.useGravity = true;

        // グローバル重力を変更（Y軸方向）
        Physics.gravity = new Vector3(0f, gravityY, 0f);
    }

    void Update()
    {
        //transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // 一定の高さより下に落ちたら自分自身を消す
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}


