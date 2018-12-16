﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    private string shotPattern;  //Recieved from the shot prefab this script is currently attached to

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
        // If it's a wall
        if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // If its an enemy's shot
        else if (shotPattern != "Player")
        {
            if (collider.CompareTag("Player"))
            {
                Destroy(gameObject);
                PlayerHealth.Instance.ReduceHP(1);

            }
        }
        // If its a player's shot
        else
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(gameObject);

            }
        }
        
    }

    // Get & set for shotPattern
    public string GetShotPattern()
    {
        return shotPattern;
    }
    public void SetShotPattern(string sp)
    {
        shotPattern = sp;
    }
}
