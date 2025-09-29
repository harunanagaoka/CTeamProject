using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnJumpPressed;


    void Start()
    {
        KeyboardInput keyboard = gameObject.AddComponent<KeyboardInput>();
        GamePadInput pad = gameObject.AddComponent<GamePadInput>();

        keyboard.OnJump += HandleJump;
        pad.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        //ƒWƒƒƒ“ƒv“ü—ÍóM
        OnJumpPressed?.Invoke();
    }


}
