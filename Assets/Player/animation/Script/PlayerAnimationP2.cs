using UnityEngine;

public class PlayerAnimationP2 : MonoBehaviour
{
    private Animator anim = null;
    bool isJumping = false;
    [SerializeField] private bool isRespawn;
    [SerializeField] private PlayerRespawn playerRespawn;

    private const string JUMP = "Jump_P2";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isRespawn = playerRespawn.isRespawning;
        if (!isRespawn)
        {
            if (!isJumping && Input.GetButtonDown(JUMP))
            {
                isJumping = true;
            }

            if (!isJumping)
            {
                float horizonralKey = Input.GetAxis("Horizontal_P2");
                float verticalKey = Input.GetAxis("Vertical_P2");


                if (horizonralKey != 0)
                {
                    anim.SetInteger("PlayerMove", 1);
                }
                else if (verticalKey != 0)
                {
                    anim.SetInteger("PlayerMove", 1);
                }
                else if (verticalKey == 0 && horizonralKey == 0)
                {
                    anim.SetInteger("PlayerMove", 0);
                }
            }
            else if (isJumping)
            {
                anim.SetInteger("PlayerMove", 2);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BeltConveyor"))
            isJumping = false;
    }
}
