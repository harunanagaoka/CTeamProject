using UnityEngine;

public class KeyboardInput : InputDeviceBase
{
    private KeyCode m_jumpKey = KeyCode.Space;

    protected override void CheckInput()
    {
        if (Input.GetKeyDown(m_jumpKey))
        {
            InvokeJump();
        }
    }

}
