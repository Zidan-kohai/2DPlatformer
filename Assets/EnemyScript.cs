using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 1;

    public GameObject deathEffect;

    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;

    Vector2 baseScale;

    Rigidbody2D rb2d;
    float moveSpeed = 5;

    private void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        float vX = moveSpeed;

        if(facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }

        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if(IsHittingWall() || IsNearEdge())
        {
            if(facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if(facingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }
        }
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if(newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    bool IsHittingWall()
    {
        bool val = false;
        
        float castDist = baseCastDist;

        if(facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;    
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
