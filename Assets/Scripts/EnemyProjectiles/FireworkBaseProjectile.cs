using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkBaseProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 target;
    private Vector2 direction;
    public float speed;

    private Vector2 startingPosition;
    private float timeElapsed;

    private GameObject targetGameObject;

    private bool isExploding = false;

    private float explosionCounter = 0.0f;
    private float childSpeed = 1.0f;

    public GameObject childDirectionalProjectilePrefab;

    public float explosionDuration = 2.0f;

    public float explosionChildCooldown = 0.1f;

    private float explosionChildCooldownCounter = 0.0f;

    public int numDirections = 10;

    public void setTarget(Vector2 target, GameObject targetPrefab) {
        Vector3 pos3D = new Vector3(target.x, target.y, 0.0f);
        this.targetGameObject = Instantiate(targetPrefab, pos3D, Quaternion.identity);
        this.direction = this.targetGameObject.transform.position - this.transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.timeElapsed = 0;
    }

    private bool isInTarget() {
        Vector2 posTarget2D = new Vector2(  this.targetGameObject.transform.position.x, 
                                            this.targetGameObject.transform.position.y);
        Vector2 posYou2D = new Vector2( this.transform.position.x, 
                                        this.transform.position.y);
        if((posTarget2D - posYou2D).SqrMagnitude() < 0.01f) {
            return true;
        } else {
            return false;
        }
    }

    void FixedUpdate()
    {
        if(this.isExploding == false) {
            float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            this.timeElapsed += Time.fixedDeltaTime;
            rb.MovePosition(rb.position + direction.normalized * this.speed * Time.fixedDeltaTime);
            if(this.isInTarget()) {
                this.explosionCounter = 0.0f;
                this.explosionChildCooldownCounter = 0.0f;
                this.isExploding = true;
                this.speed = 0.0f;
                if(this.targetGameObject != null) {
                    Destroy(this.targetGameObject);
                }
            }
        } else {
            this.explosionCounter += Time.deltaTime;
            this.explosionChildCooldownCounter += Time.deltaTime;
            if(this.explosionChildCooldownCounter >= this.explosionChildCooldown) {
                this.explosionChildCooldownCounter = 0.0f;
                this.triggerChildSpawn();
            }
            if(this.explosionCounter >= this.explosionDuration) {
                Destroy(this.gameObject);
                if(this.targetGameObject != null) {
                    Destroy(this.targetGameObject);
                }
            }
        }

    }

    private void triggerChildSpawn() {
        float step = 2 * Mathf.PI / this.numDirections;
        for(float i = 0; i < this.numDirections; i += 1) {
            float currentStep = i * step;
            GameObject projectile = Instantiate(this.childDirectionalProjectilePrefab, this.transform.position, this.transform.rotation);
            DirectionalProjectile directionalProjectile = projectile.GetComponent<DirectionalProjectile>();
            directionalProjectile.direction = new Vector2(Mathf.Cos(currentStep), Mathf.Sin(currentStep));
            directionalProjectile.speed = this.childSpeed;
        };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall")) {
            Destroy(gameObject);
            if(this.targetGameObject != null) {
                Destroy(this.targetGameObject);
            }
        } else if (collider.CompareTag("Player")) {
            Destroy(gameObject);
            if(this.targetGameObject != null) {
                Destroy(this.targetGameObject);
            }
            PlayerHealth.Instance.ReduceHP(1);
        }
    }
}
