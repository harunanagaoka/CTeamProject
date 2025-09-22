using UnityEngine;

public class BGMTest : MonoBehaviour
{
    [SerializeField]
    private MusicManager m_musicManager = null;

    [SerializeField]
    private SEManager m_seManager = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_musicManager.OnPlay(MusicManager.MusicName.ResultLoop);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_musicManager.OnStop();
        }

    }
}
