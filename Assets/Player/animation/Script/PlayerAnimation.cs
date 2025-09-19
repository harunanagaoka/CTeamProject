using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim = null;
    bool isJumping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            isJumping = true;
        }

        if (!isJumping)
        {
            float horizonralKey = Input.GetAxis("Horizontal");


            if (horizonralKey != 0)
            {
                anim.SetInteger("PlayerMove", 1);
            }
            else if (horizonralKey == 0)
            {
                anim.SetInteger("PlayerMove", 0);
            }
        }
        else if (isJumping)
        {
            anim.SetInteger("PlayerMove", 2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isJumping = false;
    }
}
