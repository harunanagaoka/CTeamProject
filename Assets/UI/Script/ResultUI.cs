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

    [SerializeField]
    private GameObject m_winnerModel_One = null;

    [SerializeField]
    private GameObject m_winnerModel_Two = null;

    [SerializeField]
    private GameObject m_drawModel_One = null;
    [SerializeField]
    private GameObject m_drawModel_Two = null;

    public void AppearResultImage()
    {
        int winnerValue = ChooseWinner();

        if (winnerValue > 0)
        {
            m_WinnerImage_One.SetActive(true);
            m_winnerModel_One.SetActive(true);
            //P1かち
        }
        else if (winnerValue < 0)
        {
            m_WinnerImage_Two.SetActive(true);
            m_winnerModel_Two.SetActive(true);
            //P2かち
        }
        else if (winnerValue == 0)
        {
            //ひきわけ
            m_drawImage.SetActive(true);
            m_drawModel_One.SetActive(true);
            m_drawModel_Two.SetActive(true);
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

    public void ResetUI()
    {
        m_retryImage.SetActive(false);
        m_backToTitleImage.SetActive(false);
        m_drawImage.SetActive(false);
        m_WinnerImage_One.SetActive(false);

        m_WinnerImage_Two.SetActive(false);
        m_winnerModel_One.SetActive(false);
        m_winnerModel_Two.SetActive(false);
        m_drawModel_One.SetActive(false);
        m_drawModel_Two.SetActive(false);
    }

}

//得点を比べる、ボタン表示関数
//〇PWin！表示さす