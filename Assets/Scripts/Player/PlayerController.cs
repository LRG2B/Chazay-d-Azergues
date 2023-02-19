using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    SpriteRenderer playerRenderer;
    Animator playerAnim;

    bool facingRight = true;

    public float maxSpeed;

    bool grounded = true;
    float groundCheckRadius = 0.2f;

    public LayerMask groundLayer;

    public Transform groundCheck;

    public float jumpPower;

    public Transform attackPoint;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); //Rigidbody -> Self
        playerRenderer = GetComponent<SpriteRenderer>(); //SpriteRenderer -> Self
        playerAnim = GetComponent<Animator>(); //Animator -> Self
        playerAnim.SetBool("HaveSword", false); //Bool HaveSword
        playerAnim.SetBool("CanMove", true); //Bool CanMove
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        playerRB.velocity = new Vector2(move * maxSpeed, playerRB.velocity.y);
        playerAnim.SetFloat("MoveSpeed", Mathf.Abs(move));

        if (move > 0 && !facingRight && playerAnim.GetBool("CanMove")) 
            Flip();
        if (move < 0 && facingRight && playerAnim.GetBool("CanMove")) 
            Flip();

        if(grounded && Input.GetAxis("Jump") > 0)
        {
            playerAnim.SetBool("IsGrounded", false);
            playerRB.velocity = new Vector2(playerRB.velocity.y, 0f);
            playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        playerAnim.SetBool("IsGrounded", grounded);

        if (playerAnim.GetBool("IsDead"))
        {
            maxSpeed = 0;
            jumpPower = 0;
            playerAnim.SetBool("CanMove", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        playerRenderer.flipX = !playerRenderer.flipX;
    }
}