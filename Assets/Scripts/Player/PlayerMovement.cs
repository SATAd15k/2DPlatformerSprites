using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;

    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float walkSpeed = 7f;


    
   
    private enum MovementState { idle, jogging, jumping, falling, fallFlipping, running, dashing, diagonalDashing, downDashing }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateAnimationState();

  
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * walkSpeed, rb.velocity.y);
 

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }


    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.jogging;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.jogging;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.fallFlipping;
        }


        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
