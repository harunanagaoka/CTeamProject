using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;

    void Start()
    {
      //  inputManager = FindObjectOfType<InputManager>();
      //なんらかの形で手に入れる

        // InputManagerのイベントを購読
        inputManager.OnJumpPressed += Jump;
    }

    private void Jump()
    {

    }

    void OnDestroy()
    {
        if (inputManager != null)
        {
            inputManager.OnJumpPressed -= Jump;
        }
    }
}
