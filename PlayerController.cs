using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float moveSpeedMultiplier = 1f;


    public bool canMove = true;
    public bool canDash = true;
    public float dash{distance = 5f;

    public float acceleration = 10f;
    public float deceleration = 15f;

    public Vector3 offset = Vector3.zero;
    public bool flipOnLeft = true;

    private Vector2 inputDirection;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Flip character based on direction
        if (flipOnLeft && inputDirection.x != 0)
        {
            spriteRenderer.flipX = inputDirection.x < 0;
        }

        // Trigger animation only when actual speed is > 0
        if (animator != null)
        {
            bool isMoving = currentVelocity.magnitude > 0.05f;
            animator.SetBool("isMoving", isMoving);


            bool notMoving = currentVelocity.magnitude == 0.0f;
            animator.SetBool("notMoving", notMoving);
        }



        // Optional: Turbo trigger
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            turbo();
        }
    }

    void FixedUpdate()
    {
        if (inputDirection.magnitude > 0.1f)
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, inputDirection * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = currentVelocity;
    }

    public void dash()
    {

    }
}

}