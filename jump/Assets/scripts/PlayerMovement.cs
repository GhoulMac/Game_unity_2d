using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anis;

    private float dirX = 0;

    [SerializeField] private float movespeed = 7f;
    [SerializeField]private float jumpforce = 7f;

    private enum MovementState { idle, running, falling, jumping }

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private AudioSource JumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anis = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed,rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded() )
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x,jumpforce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
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
            state = MovementState.falling;
        }
        anis.SetInteger("state",(int)state);
    }
    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround );
    }
}
