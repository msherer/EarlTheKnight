using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    [HideInInspector] public bool facingRight = true;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        // Invoke a Sprite component.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Invoke an Animator component
        // @TODO: Make use of this for animations
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        // Used to get the current position of our character.
        Vector2 move = Vector2.zero;

        // Used to get the horizontal control input.
        move.x = Input.GetAxis("Horizontal");

        // Used to jump and check when we're on the ground to prevent infinite jumps.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            animator.SetTrigger("playerJump");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        // Used for fliping character sprite across the Y axis.
        if (move.x > 0 && !facingRight) {
            Flip();
        } else if (move.x < 0 && facingRight) {
            Flip();
        }

        /*
         * @TODO: Figure out why flipSprite code doesn't work correctly.
         */


        //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));

        //Debug.LogError("Flip X: " + spriteRenderer.flipX + " | Move X: " + move.x + " | FlipSprite X: " + flipSprite);
        //if (flipSprite)
        //{
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        /*
         * @TODO: Setup animations for our character so we can use the animator component.
         */
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        double ep = Mathf.Abs(velocity.x) / maxSpeed;

        if (move.x > 0) {
            animator.SetTrigger("playerIdle");
        } else {
            animator.SetTrigger("playerRun");
        }

        targetVelocity = move * maxSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}