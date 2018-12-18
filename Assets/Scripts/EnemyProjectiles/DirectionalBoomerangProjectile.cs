using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBoomerangProjectile : MonoBehaviour
{
    public bool shouldFlip = true;
    Rigidbody2D rb;
    public Vector2 direction;

    public float orthogonalSpeed;

    public float acceleration;
    public float speed;

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
        if(this.shouldFlip) {
            float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        this.timeElapsed += Time.fixedDeltaTime;
        direction.Normalize();
        Vector2 orthogonalDirection = new Vector2(-1.0f * this.direction.y, this.direction.x);
        rb.MovePosition(    rb.position +
                            direction * this.speed * Time.fixedDeltaTime +
                            orthogonalDirection * this.orthogonalSpeed * Time.fixedDeltaTime);
        this.speed += this.acceleration * Time.fixedDeltaTime;
        if (this.timeElapsed > 10.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall")) {
            //Destroy(gameObject);
        } else if (collider.CompareTag("Player")) {
            //Destroy(gameObject);
            PlayerHealth.ReduceHP(1);
        }
    }
}
