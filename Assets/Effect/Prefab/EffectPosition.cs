using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class EffectPosition : MonoBehaviour
{
    Vector3 koteiRot = Vector3.zero;
    float kotei = 0;

    void Update()
    {
        this.gameObject.transform.position = transform.parent.position;
        this.gameObject.transform.rotation = Quaternion.identity;
    }
}
