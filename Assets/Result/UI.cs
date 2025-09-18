using UnityEngine;
using UnityEngine.UI; // 追加

public class UI : MonoBehaviour
{
    public static int player1Score;
    public static int player2Score;

    [SerializeField] private Sprite P1winner;
    [SerializeField] private Sprite P2winner;
    [SerializeField] private GameObject winnerImageObject; // 画像オブジェクトをアサイン

    void Update()
    {

    }

    void ShowWinner()
    {
        int p1 = Score1.player1Score;
        int p2 = Score1.player2Score;

        if (p1 > p2)
        {
            winnerImageObject.SetActive(true); // 表示
            winnerImageObject.GetComponent<Image>().sprite = P1winner;
        }
        else if (p2 > p1)
        {
            winnerImageObject.SetActive(true); // 表示
            winnerImageObject.GetComponent<Image>().sprite = P2winner;
        }
        else
        {
            winnerImageObject.SetActive(false); // 引き分け時は非表示
        }
    }
}
