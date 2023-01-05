using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D collide;
    SpriteRenderer spriteRenderer;
    Animator anim;

    int jumpCount;

    public int maxSpeed = 5;
    public int jumpPower = 5;
    public int jumpMaxCount = 2;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collide = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Stop Walking
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isRun", false);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMaxCount)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            anim.Play("Player_Jump", -1, 0f);

            jumpCount++;
        }

        // Flip Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            anim.SetBool("isRun", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        float direction = Mathf.Sign(rigid.velocity.x);

        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed * direction, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            jumpCount = 0;
            anim.SetBool("isJump", false);
        }
    }


}
