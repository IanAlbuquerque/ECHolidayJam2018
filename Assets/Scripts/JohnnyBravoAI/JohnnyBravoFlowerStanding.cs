using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnnyBravoFlowerStanding : MonoBehaviour, EnemyAttackPattern
{
    public GameObject directionalProjectilePrefab;
    public float speed = 1;
    private bool isRunning = false;
    public float shootCooldown = 0.5f;
    public float projectileSpeed = 1.0f;
    private float shootCounter = 0.0f;

    public int numDirections = 10;

    public float timeElapsed = 0.0f;


    public void startAttackPattern() {
        this.isRunning = true;
        this.shootCounter = 0.0f;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
    }

    public AudioSource jbShoot;
    private void shoot() {
        float step = 2 * Mathf.PI / this.numDirections / 4.0f;
        jbShoot.Play();
        for (float i = 0; i < this.numDirections; i += 1) {
            float currentStep = i * step + (3 * Mathf.PI / 4.0f);
            GameObject projectile = Instantiate(this.directionalProjectilePrefab, this.transform.position, this.transform.rotation);
            DirectionalProjectile directionalProjectile = projectile.GetComponent<DirectionalProjectile>();
            directionalProjectile.direction = new Vector2(Mathf.Cos(currentStep), Mathf.Sin(currentStep));
            directionalProjectile.speed = this.projectileSpeed;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isRunning) {
            this.timeElapsed += Time.deltaTime;
            this.shootCounter += Time.deltaTime;
            if(this.shootCounter > this.shootCooldown) {
                this.shootCounter = 0.0f;
                this.shoot();
            }
        }
    }
}
