using UnityEngine;
using Unity.Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    public DeathScreen deathScreen;
    public bool inputEnabled = false;
    public float direction;
    public float speed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded;
    public bool isJumping;
    public Rigidbody2D rb2D;
    public Collider2D collider;
    public float InvincibilityDuration = 1f;
    public bool isInvincible = false;
    public Transform PlayerSprite;
    public Transform ParryHitbox;

    public float DashCooldown = 2f;
    public float DashForce = 10f;
    public float DashUnmultiplier = 0.7f;
    public float DashUnmultiplier2 = 0.2f;
    public float deathLaucnhForce = 20f;

   
    private float dashTime;
    public float dashDuration = 0.2f;
    private bool isDashing = false;

    public bool CanDoubleJump = false;
    public float GravityDemultiplier = 0.4f;


    public CinemachineCamera cam;
void Start()
    {
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();
        if (collider == null)
            collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (rb2D.linearVelocity.sqrMagnitude < 0.001f && isGrounded)
        {
          
            PlayerSprite.GetComponent<PlayerSprite>().IdleAnim();
        }
        else if (rb2D.linearVelocity.sqrMagnitude > 0.001f && isGrounded)
        {

            PlayerSprite.GetComponent<PlayerSprite>().EndDoubleJumpAnim();
        }
        if (DashCooldown > 0)
            DashCooldown -= Time.deltaTime;

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
            }
            return; 
        }
        if (inputEnabled)
        {
            direction = Input.GetAxisRaw("Horizontal");
            rb2D.linearVelocity = new Vector2(direction * speed, rb2D.linearVelocity.y);

            if (direction != 0)
            {
                PlayerSprite.GetComponent<PlayerSprite>().Wobble();
            }
            else
            {
                PlayerSprite.GetComponent<PlayerSprite>().Stabilize();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
                isJumping = true;
                isGrounded = false;
                CanDoubleJump = true;
            }
            else if (Input.GetButtonDown("Jump") && isJumping && !isGrounded && CanDoubleJump)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce * 0.4f);
                isJumping = true;
                isGrounded = false;
                CanDoubleJump = false;
                rb2D.gravityScale = GravityDemultiplier;
                PlayerSprite.GetComponent<PlayerSprite>().DoubleJumpAnim();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
                PlayerSprite.GetComponent<PlayerSprite>().EndDoubleJumpAnim();
            }
        }            
      
      
    }

    public void Parry()
    {
        // Implement parry logic here
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            
            rb2D.gravityScale = 1f;
            isGrounded = true;
            CanDoubleJump = false;
            isJumping = false;
            PlayerSprite.GetComponent<PlayerSprite>().EndDoubleJumpAnim();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (other.gameObject.GetComponent<Obstacle>() != null && other.gameObject.GetComponent<Obstacle>().DoDedSpin)
            {
                rb2D.AddForce(Vector2.up * deathLaucnhForce, ForceMode2D.Impulse);
                PlayerSprite.GetComponent<PlayerSprite>().DieAnim();
            }
            Lose();
        }
    }

    public void Lose()
    {
        if (!isInvincible)
        {
            inputEnabled = false;
            cam.Follow = null;
            Debug.Log("Player has lost!");
            isInvincible = true;
            if (deathScreen != null) deathScreen.Show();

        }
    }

    public void Dash()
    {
        if (DashCooldown <= 0)
        {
            rb2D.gravityScale = 0f;
            isDashing = true;
            dashTime = dashDuration;

           
            float dashDir = direction != 0 ? direction : 1f;

            if (isGrounded)
            {
                rb2D.linearVelocity = new Vector2(0f, rb2D.linearVelocity.y);
                rb2D.AddForce(new Vector2(dashDir, 0).normalized * DashForce, ForceMode2D.Impulse);
            }
            else
            {
                rb2D.AddForce(new Vector2(dashDir, 0).normalized * DashForce * DashUnmultiplier, ForceMode2D.Impulse);
                rb2D.AddForce(Vector2.up * DashForce * DashUnmultiplier2, ForceMode2D.Impulse);
            }
            DashCooldown = 2f;
            rb2D.gravityScale = 1f;
        }
    }

}