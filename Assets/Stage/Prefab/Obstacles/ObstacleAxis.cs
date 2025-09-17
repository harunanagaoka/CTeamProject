using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleAxis : MonoBehaviour
{
    [SerializeField]
    float m_rotateVelocity = 0f;

    [SerializeField]
    float m_moveVelocity = 0f;

    Vector3 m_moveDirection = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_moveDirection = new Vector3(0,0,m_moveVelocity);
        StartCoroutine(RotateSemiCircle());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RotateSemiCircle()
    {
        float angle = 180;

        while (angle > 0)//180“x‰ñ‚é‚Ü‚Å 
        {
            this.transform.RotateAround(this.transform.position,-Vector3.right,m_rotateVelocity);
            angle -= m_rotateVelocity;
            yield return null;
        }
        StartCoroutine(MoveConveyor());
    }

    private IEnumerator MoveConveyor()
    {
        
        Vector3 pos = this.transform.position;

        pos = pos - m_moveDirection;

        this.transform.position = pos;



            yield return null;

    }

}
