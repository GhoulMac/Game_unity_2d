using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anis;

    private float dirX = 0;

    [SerializeField] private float movespeed = 7f;
    [SerializeField]private float jumpforce = 14f;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anis = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed,rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpforce);
        }
        if (dirX > 0f)
        {
            anis.SetBool("running", true);
        }
        else if (dirX < 0f)
        {
            anis.SetBool("running", true);
        }
        else 
        {
            anis.SetBool("running", false);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            anis.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anis.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anis.SetBool("running", false);
        }
    }
}
