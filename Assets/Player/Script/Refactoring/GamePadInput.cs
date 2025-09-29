using UnityEngine;

public class GamePadInput : InputDeviceBase
{
    protected override void CheckInput()
    {
        //if(ゲームパッド入力){
        InvokeJump();
        //}
        
    }
}
