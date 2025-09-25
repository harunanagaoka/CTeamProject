using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private ScoreManager m_scoreManager = null;

    [SerializeField]
    private GameObject m_retryImage = null;

    [SerializeField]
    private GameObject m_backToTitleImage = null;

    [SerializeField]
    private GameObject m_drawImage = null;

    [SerializeField]
    private GameObject m_WinnerImage_One = null;

    [SerializeField]
    private GameObject m_WinnerImage_Two = null;

    public void AppearResultImage()
    {
        int winnerValue = ChooseWinner();

        if (winnerValue > 0)
        {
            m_WinnerImage_One.SetActive(true);
            //P1かち
        }
        else if (winnerValue < 0)
        {
            m_WinnerImage_Two.SetActive(true);
            //P2かち
        }
        else if (winnerValue == 0)
        {
            //ひきわけ
            m_drawImage.SetActive(true);
        }
    }

    private int ChooseWinner()
    {
        int score_One = m_scoreManager.PlayerOneScore;
        int score_Two = m_scoreManager.PlayerTwoScore;

        return score_One - score_Two;
    }

    public void AppearBottuns()
    {
        m_retryImage.SetActive(true);
        m_backToTitleImage.SetActive(true);
    }

}

//得点を比べる、ボタン表示関数
//〇PWin！表示さす