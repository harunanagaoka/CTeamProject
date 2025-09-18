using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 180f;

    [SerializeField]
    private float gravityY = -9.81f; // Inspectorで重力の強さを調整可能

    private Rigidbody rb;

    public Vector3[] spawnPositions = new Vector3[10]; // 座標を10個保持

    void Awake()
    {
        // 例：座標を手動で設定（実際はInspectorで設定するのが便利）
        spawnPositions[0] = new Vector3(0f, 0f, 0f); // x方向に並べて高さ10から降らせる
        spawnPositions[1] = new Vector3(0f, 0f, 0f);
        spawnPositions[2] = new Vector3(0f, 0f, 0f);
        spawnPositions[3] = new Vector3(0f, 0f, 0f);
        spawnPositions[4] = new Vector3(0f, 0f, 0f);
        spawnPositions[5] = new Vector3(0f, 0f, 0f);
        spawnPositions[6] = new Vector3(0f, 0f, 0f);
        spawnPositions[7] = new Vector3(0f, 0f, 0f);
        spawnPositions[8] = new Vector3(0f, 0f, 0f);
        spawnPositions[9] = new Vector3(0f, 0f, 0f);
        //（）の中の数値を変えると座標が変わるよ
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
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // 一定の高さより下に落ちたら自分自身を消す
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}


