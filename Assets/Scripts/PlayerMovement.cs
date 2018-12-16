using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float movementSpeed = 2.0f;
    private Vector2 currentSpeed = new Vector2(0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 goalSpeed = direction * this.movementSpeed * Time.deltaTime; 
        rigidbody2D.velocity = goalSpeed;
    }
}
