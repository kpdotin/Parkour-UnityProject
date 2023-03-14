using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    public float playerSpeed=4;
    public float jumpForce ;
    public float fallForce = 1.5f;
    private Animator anim;
    private int count;
    public TMP_Text score;
    public Transform respawnpoint; 
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public float raycastLength = 2;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        respawnpoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 5)
        {
            SceneManager.LoadScene("EndScene");
        }
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        score.text = "Your Score: " + count;
        horizontal = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            anim.SetBool("Jump", true);
            
        }
        else
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallForce);
        }
        if (rb.velocity.x != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        //if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    anim.SetBool("Running", false);
        //    anim.SetBool("Jump", true);
        //}
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sr.flipX = true;
        }
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.cyan);


    }


    private void respawn()
    {
        transform.position = respawnpoint.position;
        count -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            count++;
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Respawn")
        {
            respawn();
        }
    }

}
