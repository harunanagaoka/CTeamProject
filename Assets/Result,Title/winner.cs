using UnityEngine;

public class winner : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private Transform centerPoint; // 中央の位置（空のGameObjectを用意してアサイン）

    private bool isShown = false;

    void Update()
    {
        if (remainingtime.IsTimeOver && !isShown)
        {
            ShowWinnerCharacter();
            isShown = true;
        }
    }

    void ShowWinnerCharacter()
    {
        int p1 = Score1.player1Score;
        int p2 = Score1.player2Score;

        GameObject winnerObj1 = null;
        GameObject winnerObj2 = null;

        if (p1 > p2)
        {
            winnerObj1 = Instantiate(player1Prefab, centerPoint.position, Quaternion.identity);
        }
        else if (p2 > p1)
        {
            winnerObj2 = Instantiate(player2Prefab, centerPoint.position, Quaternion.identity);
        }
        else
        {
            winnerObj1 = Instantiate(player2Prefab, centerPoint.position, Quaternion.identity);
            winnerObj2 = Instantiate(player1Prefab, centerPoint.position, Quaternion.identity);
        }

        // 必要に応じてwinnerObjのスケールや向きを調整
    }
}
