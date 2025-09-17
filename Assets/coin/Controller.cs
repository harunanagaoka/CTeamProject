using UnityEngine;

public class Controller : MonoBehaviour
{
    void Update()
    {
        if (!Timer.IsGameStarted) return;
        transform.Translate(Vector3.right * Time.deltaTime * 2f);
    }
}
