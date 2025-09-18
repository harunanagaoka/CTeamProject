using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    [SerializeField] private Vector3 conveyorDirection = Vector3.right;
    [SerializeField] private float conveyorSpeed = 3f;

    // ベルト速度を取得する用
    public Vector3 GetConveyorVelocity()
    {
        return conveyorDirection.normalized * conveyorSpeed;
    }
}
