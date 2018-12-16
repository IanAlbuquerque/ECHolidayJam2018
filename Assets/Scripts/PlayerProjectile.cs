using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;

    private Vector2 startingPosition;
    private float timeElapsed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.timeElapsed = 0;
    }

    void FixedUpdate()
    {
        this.timeElapsed += Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If it's a wall
        if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // If it's an enemy
        else if (collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);

        }    
    }
}
