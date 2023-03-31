using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private float horizontal, vertical;
    private Vector2 position;
    private Rigidbody2D rgdBody;
    private BoxCollider2D boxCollider;
    private float wallJump;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float jump;
    public Animator Anime;

    private void Awake()
    {
        rgdBody = gameObject.GetComponent<Rigidbody2D>();
        Anime = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Jump");
        Anime.SetBool("Run", horizontal != 0);
        Anime.SetBool("isGrounded", isGrounded());
        playerMove();
        playerJump();
    }

    private void playerMove()
    {
        Vector2 Scale = transform.localScale;

        if (horizontal > 0)
            Scale.x = 1f * Mathf.Abs(Scale.x);
        else if (horizontal < 0)
            Scale.x = -1f * Mathf.Abs(Scale.x);

        transform.localScale = Scale;

        position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

        if (horizontal > 0 && !onWall() && !isGrounded())
        {
            rgdBody.gravityScale = 1;
            //rgdBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 1);*            
        }
    }

    private void playerJump()
    {   
        if (vertical > 0)
        {
            if (isGrounded())
            {
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, jump);
            }

            if (onWall() && !isGrounded())
            {
                rgdBody.gravityScale = 0;
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, speed);
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,Vector2.down, 0.1f,groundLayer);
        return raycasthit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycasthit.collider != null;
    }
}
