using UnityEngine;
using UnityEngine.UI; // 追加

public class UI : MonoBehaviour
{
    public static int player1Score;
    public static int player2Score;
    public static bool isShown = false;

    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private Transform centerPoint; // 中央の位置（空のGameObjectを用意してアサイン）

    [SerializeField] private Sprite P1winner;
    [SerializeField] private Sprite P2winner;
    [SerializeField] private Sprite Draw; // 引き分け用の画像をアサイン
    [SerializeField] private GameObject winnerImageObject; // 画像オブジェクトをアサイン

    void Update()
    {
        if (remainingtime.IsTimeOver && !isShown)
        {
            ShowWinner();
            isShown = true;
        }
    }

    void ShowWinner()
    {
        //int p1 = Score1.player1Score;
        //int p2 = Score1.player2Score;

        //GameObject winnerObj1 = null;
        //GameObject winnerObj2 = null;

        //if (p1 > p2)
        //{
        //    winnerImageObject.SetActive(true);
        //    winnerImageObject.GetComponent<Image>().sprite = P1winner;//UI
        //    winnerObj1 = Instantiate(player1Prefab, centerPoint.position, Quaternion.identity);//キャラクター表示
        //}
        //else if (p2 > p1)
        //{
        //    winnerImageObject.SetActive(true);
        //    winnerImageObject.GetComponent<Image>().sprite = P2winner;//UI
        //    winnerObj2 = Instantiate(player2Prefab, centerPoint.position, Quaternion.identity);//キャラクター表示
        //}
        //else
        //{
        //    // 引き分け時もDraw画像を表示
        //    winnerImageObject.SetActive(true);
        //    winnerImageObject.GetComponent<Image>().sprite = Draw;//UI
        //    winnerObj1 = Instantiate(player2Prefab, centerPoint.position, Quaternion.identity);
        //    winnerObj2 = Instantiate(player1Prefab, centerPoint.position, Quaternion.identity);//引き分けの場合、どちらも表示
        //}
    }
}
