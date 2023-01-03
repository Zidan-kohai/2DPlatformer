using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{   

    public float speed = 5f;
    private float Move;
    public float jump = 20f;
    
    public bool Grounded = false;
    public Transform GroundCheck;
    public float GroundRadius = 0.2f;
    public LayerMask wtfIsGround;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    private void Update()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, wtfIsGround);

        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Previouslevel")
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
    }
}