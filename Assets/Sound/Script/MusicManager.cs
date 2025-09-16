using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] m_audioSources;

    [SerializeField]
    private AudioClip[] m_audioClips;

    public enum MusicName
    {
        SEName
    }

    public void OnPlay(MusicName musicNum)
    {
        m_audioSources[(int)musicNum].clip = m_audioClips[(int)musicNum];
        m_audioSources[(int)musicNum].loop = true;
        m_audioSources[(int)musicNum].Play();

    }
    public void OnStop(MusicName musicNum)
    {
        m_audioSources[(int)(musicNum)].Stop();
    }
}