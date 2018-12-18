using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;

    private Vector2 startingPosition;
    private float timeElapsed;

    public PlayerHealth PlayerHealth;

    void Start()
    {
        this.PlayerHealth = (PlayerHealth) FindObjectsOfType(typeof(PlayerHealth))[0];
        rb = GetComponent<Rigidbody2D>();
        this.startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.timeElapsed = 0;
    }

    void FixedUpdate()
    { 
        this.timeElapsed += Time.fixedDeltaTime;
        Vector2 targetPosition = new Vector2(this.startingPosition.x + velocity.x * this.timeElapsed,
                                                this.startingPosition.y + velocity.y * Mathf.Sin(velocity.x * 5 * this.timeElapsed));

        rb.MovePosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If it's a wall
        if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // If it's the player
        else if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerHealth.ReduceHP(1);

        }
    }
}
