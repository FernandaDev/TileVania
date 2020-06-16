using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Collider2D bodyCollider;
    Collider2D feetCollider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,25f);

    bool isAlive = true;

    float defaultGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        feetCollider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        defaultGravity = rb.gravityScale;
    }

    private void Update()
    {
        if (!isAlive)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    isAlive = true;
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //}
            return; 
        }

        Run();
        Jump();
        Flip();
        ClimbLadder();
        Die();
    }

    void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, rb.velocity.y);

        rb.velocity = playerVelocity;

        anim.SetBool("IsRunning", HasHorizontalVelocity());
    }

    void ClimbLadder()
    {
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            anim.SetBool("IsClimbing", false);
            rb.gravityScale = defaultGravity;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);

        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        anim.SetBool("IsClimbing", HasVerticalVelocity());

    }

    void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 targetJumpVelocity = new Vector2(0f, jumpSpeed);
            rb.velocity += targetJumpVelocity;
        }

    }

    void Flip()
    {
        if (HasHorizontalVelocity())
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Die");
            rb.velocity = deathKick;
            GameSession.instance.ProcessPlayerDeath();
        }
    }

    bool HasHorizontalVelocity()
    {
        return Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
    }

    bool HasVerticalVelocity()
    {
        return Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
    }

}
