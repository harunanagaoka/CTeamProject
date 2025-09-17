using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadLogger : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current == null) return;

        // Face Buttons
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            Debug.Log("A / × ボタンが押されました");

        if (Gamepad.current.buttonEast.wasPressedThisFrame)
            Debug.Log("B / ○ ボタンが押されました");

        if (Gamepad.current.buttonWest.wasPressedThisFrame)
            Debug.Log("X / □ ボタンが押されました");

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            Debug.Log("Y / △ ボタンが押されました");

        // Shoulder Buttons
        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
            Debug.Log("L1 / LB ボタンが押されました");

        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
            Debug.Log("R1 / RB ボタンが押されました");

        // Trigger Buttons
        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
            Debug.Log("L2 / LT トリガーが押されました");

        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
            Debug.Log("R2 / RT トリガーが押されました");

        // Stick Buttons
        if (Gamepad.current.leftStickButton.wasPressedThisFrame)
            Debug.Log("Lスティック押し込みが押されました");

        if (Gamepad.current.rightStickButton.wasPressedThisFrame)
            Debug.Log("Rスティック押し込みが押されました");

        // D-Pad
        if (Gamepad.current.dpad.up.wasPressedThisFrame)
            Debug.Log("十字キー 上 が押されました");

        if (Gamepad.current.dpad.down.wasPressedThisFrame)
            Debug.Log("十字キー 下 が押されました");

        if (Gamepad.current.dpad.left.wasPressedThisFrame)
            Debug.Log("十字キー 左 が押されました");

        if (Gamepad.current.dpad.right.wasPressedThisFrame)
            Debug.Log("十字キー 右 が押されました");

        // Menu Buttons
        if (Gamepad.current.startButton.wasPressedThisFrame)
            Debug.Log("スタート / メニュー ボタンが押されました");

        if (Gamepad.current.selectButton.wasPressedThisFrame)
            Debug.Log("セレクト / ビュー ボタンが押されました");

        // Optional: Stick Movement Logging (not just button press)
        Vector2 leftStick = Gamepad.current.leftStick.ReadValue();
        Vector2 rightStick = Gamepad.current.rightStick.ReadValue();

        if (leftStick != Vector2.zero)
            Debug.Log($"Lスティック移動: {leftStick}");

        if (rightStick != Vector2.zero)
            Debug.Log($"Rスティック移動: {rightStick}");
    }
}

