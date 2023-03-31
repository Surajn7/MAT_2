using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timerAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    private float setTimer = 20;
    private float horizontal, vertical;
    private Vector2 position;
    private Rigidbody2D body;
    private bool onWall;
    private bool isGrounded;
    public float jump;
    public Animator Anime;


    private void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        Anime = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Anime.SetBool("Run", horizontal != 0);
        Anime.SetBool("isGrounded", isGrounded);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Jump");
        playerMove(horizontal);
        playerJump(vertical);
        WallJump();
        setTimer += Time.deltaTime;
        Attack();
    }

    private void playerMove(float horizontal)
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
    }

    private void playerJump(float vertical)
    {
        if (vertical > 0 && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            isGrounded = false;
        }
    }

    private void WallJump()
    {
        if (onWall && !isGrounded)
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        if (onWall && !isGrounded && vertical > 0)
        {
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1, 3);
            body.gravityScale = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D enterCollision)
    {
        if (enterCollision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (enterCollision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
            onWall = true;
            body.gravityScale = 0;
            WallJump();
        }
    }

    private void OnCollisionExit2D(Collision2D exitCollision)
    {
        if (exitCollision.gameObject.CompareTag("Wall"))
        {
            body.gravityScale = 1;
            onWall = false;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0) && setTimer > timerAttack && !onWall && isGrounded)
        { 
            Anime.SetTrigger("Attack");
            setTimer = 0;
            fireBalls[0].transform.position = firePoint.position;
            fireBalls[0].GetComponent<Attack_Fire>().setDirection(Mathf.Sign(transform.localScale.x));
        }        
    }

    private int findfireBalls()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}