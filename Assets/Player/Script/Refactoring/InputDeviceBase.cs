using System;
using UnityEngine;

public abstract class InputDeviceBase : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnMove;

    protected abstract void CheckInput();

    void Update()
    {
        CheckInput();
    }

    protected void InvokeJump()
    {
        OnJump?.Invoke();
    }

    protected void InvokeMove()
    {
        OnMove?.Invoke();
    }
}
