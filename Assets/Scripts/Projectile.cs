using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    public string shotPattern;  //Recieved from the shot prefab this script is currently attached to
    public PlayerHealth ph;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    { 
        if(shotPattern == "Player")
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        else if(shotPattern == "Medusa")
        {
            //Vector2 movement = new Vector2(rb.position.x, rb.position.x * rb.position.x);
            rb.MovePosition(rb.position /*+ movement*/+ velocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If it's NOT a wall
        if (! collider.CompareTag("Wall"))
        {
            // If its an enemy's shot
            if (shotPattern != "Player")
            {
                if (collider.CompareTag("Player"))
                {
                    ph.ReduceHP(1);

                }
            }
            // If its a player's shot
            else
            {
                if (collider.CompareTag("Enemy"))
                {

                }
            }
        }
        Destroy(gameObject);
    }

}
