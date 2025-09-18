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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.one);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.two);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.three);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.four);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.five);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.six);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.seven);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.eight);
        }

        if (Input.GetKeyDown(KeyCode.P)){
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.nine);
        }
    }
}
