using UnityEngine;

public class Score1 : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon_reach;

    // プレイヤーごとのスコア
    public static int player1Score = 0;
    public static int player2Score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー1に触れた場合
        if (other.CompareTag("Player1"))
        {
            player1Score += 1;
            Destroy(gameObject); // コインを消す
        }
        // プレイヤー2に触れた場合
        else if (other.CompareTag("Player2"))
        {
            player2Score += 1;
            Destroy(gameObject); // コインを消す
        }
    }
}
