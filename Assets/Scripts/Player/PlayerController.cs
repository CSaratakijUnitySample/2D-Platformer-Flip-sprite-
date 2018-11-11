using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveForce;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravity;

    [SerializeField]
    float maxVerticalForce;

    [SerializeField]
    Transform ground;

    [SerializeField]
    LayerMask groundLayer;


    bool isCanJump;
    bool isPressedJump;
    bool isFacingRight;

    Vector2 inputVector;
    Vector2 velocity;
    Vector2 rayDirection;

    RaycastHit2D hit;
    Rigidbody2D rigid;


    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
        FlipHandler();
    }

    void FixedUpdate()
    {
        CheckGround();
        MovementHandler();
    }

    void Initialize()
    {
        isPressedJump = false;
        isCanJump = false;
        isFacingRight = true;
        rayDirection = new Vector2(1.0f, -1.0f);
        rigid = GetComponent<Rigidbody2D>();
    }

    void InputHandler()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        isPressedJump = Input.GetButtonDown("Jump");
    }

    void CheckGround()
    {
        rayDirection.x *= transform.localScale.x;
        hit = Physics2D.Raycast(ground.position, rayDirection, 0.2f, groundLayer);
        isCanJump = (hit.collider != null);
    }

    void MovementHandler()
    {
        velocity.x = (moveForce * inputVector.x);

        if (isCanJump && isPressedJump) {
            velocity.y = jumpForce;
        }
        else {
            velocity.y -= gravity;
            velocity.y = Mathf.Clamp(velocity.y, -maxVerticalForce, jumpForce);
        }

        rigid.velocity = (velocity * Time.deltaTime);
    }

    void FlipHandler()
    {
        if (inputVector.x > 0.0f && !isFacingRight) {
            FlipFacingDirection();
        }
        else if (inputVector.x < 0.0f && isFacingRight) {
            FlipFacingDirection();
        }
    }

    void FlipFacingDirection()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1.0f;
        transform.localScale = newScale;
    }
}
