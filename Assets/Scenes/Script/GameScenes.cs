using UnityEngine;

public enum GameScene
{
    Title,
    Main,
    Result

}
public class GameScenes : MonoBehaviour
{
    [SerializeField]
    private GameScene m_currentScene = GameScene.Title;

    public GameScene CurrentScene {  get { return m_currentScene; } private set { m_currentScene = value; } }

    public void ChangeScene(GameScene newScene)
    {
        m_currentScene = newScene;
    }

}
