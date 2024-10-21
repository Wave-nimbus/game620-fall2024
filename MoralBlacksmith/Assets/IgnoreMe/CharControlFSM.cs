using UnityEngine;

public class CharControlFSM : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private MoveState currMoveState;
    [SerializeField]
    private Rigidbody2D rb;

    private float walkSpeed;
    private float runSpeed;
    private float jumpStrength;
    private bool isGrounded;

    

    void Start()
    {
        currMoveState = MoveState.idle;
        walkSpeed = 2f;
        runSpeed = 5f;
        jumpStrength = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currMoveState)
        {
            case MoveState.idle:
                DoIdle(); 

                if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    currMoveState = MoveState.walk;
                }
                else if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currMoveState = MoveState.jump;
                }
                else if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currMoveState = MoveState.duck;
                }
                break;

            case MoveState.walk:
                DoWalk();

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    currMoveState = MoveState.run;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currMoveState = MoveState.jump;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currMoveState = MoveState.duck;
                }
                else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    currMoveState = MoveState.idle;
                }
                break;

            case MoveState.run:
                DoRun();

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    currMoveState = MoveState.walk;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currMoveState = MoveState.jump;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currMoveState = MoveState.duck;
                }
                else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    currMoveState = MoveState.idle;
                }
                break;

            case MoveState.duck:
                DoDuck();
                if(Input.GetKeyUp(KeyCode.DownArrow))
                {
                    currMoveState = MoveState.idle;
                    transform.localScale = Vector3.one;
                }
                break;

            case MoveState.jump:
                DoJump();
                currMoveState = MoveState.fall;
                break;

            case MoveState.fall:
                if(isGrounded)
                {
                    currMoveState = MoveState.idle;
                }
                break;

            default:
                currMoveState = MoveState.idle;
                break;

        }
    }

    private void DoIdle()
    {
        transform.Translate(Vector3.zero);
    }

    private void DoWalk()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-Vector3.right * walkSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
        }
    }

    private void DoRun()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-Vector3.right * runSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
        }
    }

    private void DoDuck()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
    }

    private void DoJump()
    {
        rb.AddForce(new Vector2(walkSpeed * 100, jumpStrength * 100));
        isGrounded = false;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

}

public enum MoveState
{
    idle, walk, run, duck, jump, fall
};

