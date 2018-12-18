using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject centerObject;
    public float speed;
    public float radius;

    public float angle;

    private Vector2 startingPosition;
    private float timeElapsed;

    public PlayerHealth PlayerHealth;

    void Start()
    {
        this.PlayerHealth = (PlayerHealth) FindObjectsOfType(typeof(PlayerHealth))[0];
        rb = GetComponent<Rigidbody2D>();
        this.timeElapsed = 0;
    }

    void FixedUpdate()
    {
        this.angle += this.speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.AngleAxis(this.angle / (2*Mathf.PI) * 360.0f - 90.0f, Vector3.back);
        this.timeElapsed += Time.fixedDeltaTime;
        Vector2 center = new Vector2(this.centerObject.transform.position.x, this.centerObject.transform.position.y);
        rb.MovePosition(center + new Vector2(Mathf.Sin(this.angle), Mathf.Cos(this.angle)) * this.radius);
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
