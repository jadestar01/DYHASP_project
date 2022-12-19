using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int maxHealth = 3;
    public int Health = 3;
    public int saveHealth;
    public int maxCoin;
    public int coinCount = 0;
    public Vector2 SavePoint;

    public float moveSpeed = 5.0f;

    public float jumpPower = 5.0f;
    public float jumpingGravity = 1.5f;
    public float faliingGravity = 1.0f;
    public LayerMask mask;
    bool isGrounded;
    int dir;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    Vector2 move;

    private void Start()
    {
        //Set up
        dir = 0;
        isGrounded = true;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        SavePoint = transform.position;
        saveHealth = Health;
    }
    void Update()
    {
        GroundedCheck();
        MoveFunc();
        MoveButton();
        JumpFunc();
        GravityController();
        GetDamaged();
    }

    void GetDamaged()
    {
        if (Health != saveHealth)
        {
            if (Health > saveHealth)
            {
                Debug.Log("체력 회복 됨!");
            }
            else if (Health < saveHealth)
            {
                Debug.Log("체력 소모 됨!");
                transform.position = SavePoint;
            }

            saveHealth = Health;
        }
        if (Health == 0)
        {
            Debug.Log("Game Over");
        }
    }

    void MoveFunc()
    {
        //Move
        move = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rigid.velocity.y);
        rigid.velocity = move;
        //Flip
        if (Input.GetAxisRaw("Horizontal") == 1)
            spriteRenderer.flipX = false;
        else if (Input.GetAxisRaw("Horizontal") == -1)
            spriteRenderer.flipX = true;
    }

    public void ButtonUp()
    {
        this.dir = 0;
    }

    public void ButtonDown(int dir)
    {
        this.dir = dir;
    }

    public void MoveButton()
    {
        if (dir != 0)
        {
            move = new Vector2(dir * moveSpeed, rigid.velocity.y);
            rigid.velocity = move;
            if (dir == 1)
                spriteRenderer.flipX = false;
            else if (dir == -1)
                spriteRenderer.flipX = true;
        }
    }

    void JumpFunc()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(new Vector2(0, 1 * jumpPower), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void JumpButton()
    {
        if (isGrounded)
        {
            rigid.AddForce(new Vector2(0, 1 * jumpPower), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void a()
    {
        Debug.Log("aaa");
    }

    void GravityController()
    {
        if (!isGrounded)                    //착지 상태가 아니라면,
        {
            if (rigid.velocity.y > 0)       //상승 중이라면,
            {
                rigid.gravityScale = jumpingGravity;
            }
            else                            //낙하 중이라면,
            {
                rigid.gravityScale = faliingGravity;
            }
        }
        else                                //착지 상태라면,
        {
            rigid.gravityScale = 1.0f;
        }
    }

    void GroundedCheck()
    {
        Debug.DrawRay(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down * 0.6f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.down * 0.6f, Color.red);

        bool ray1 = false;
        bool ray2 = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y),
                                Vector2.down,
                                0.6f,
                                LayerMask.GetMask("Wall", "Cannon"))) ray1 = true;
        if (Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y),
                                Vector2.down,
                                0.6f,
                                LayerMask.GetMask("Wall", "Cannon"))) ray2 = true;

        if (!ray1 && !ray2)
        { isGrounded = false; }
        else
        { isGrounded = true; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            coinCount++;
        }
        if (collision.tag == "SavePoint")
        {
            SavePoint = collision.transform.position;
        }
        if (collision.tag == "Spike")
        {
            Health--;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Health")
        {
            if (Health < 3)
            {
                Health++;
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "Trampolin")
        {
            jumpPower = 10.0f;
        }
        if (collision.tag == "In")
        {
            transform.position = collision.GetComponent<Portal>().Out.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Trampolin")
        {
            jumpPower = 5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            Health--;
            Destroy(collision.gameObject);
        }
    }
}