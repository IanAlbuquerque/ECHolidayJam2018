using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnnyBravoGattling : MonoBehaviour, EnemyAttackPattern
{
    public GameObject directionalProjectilePrefab;
    public float speed = 1;
    private bool isRunning = false;
    public float shootCooldown = 0.5f;
    public float projectileSpeed = 1.0f;
    private float shootCounter = 0.0f;

    public int numDirections = 10;

    public float timeElapsed = 0.0f;

    public float angleRandomRange = 30;


    public void startAttackPattern() {
        this.isRunning = true;
        this.shootCounter = 0.0f;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
    }

    private void shoot() {
        Vector3 playerPosition3D = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector2 playerPosition2D = new Vector2(playerPosition3D.x, playerPosition3D.y);
        Vector2 enemyPosition2D = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 direction = playerPosition2D - enemyPosition2D;
        for(float i = 0; i < this.numDirections; i += 1) {
            Vector2 randomizedDirection = Quaternion.AngleAxis(Random.Range(-angleRandomRange, angleRandomRange), Vector3.forward) * direction;
            GameObject projectile = Instantiate(this.directionalProjectilePrefab, this.transform.position, this.transform.rotation);
            DirectionalProjectile directionalProjectile = projectile.GetComponent<DirectionalProjectile>();
            directionalProjectile.direction = randomizedDirection.normalized;
            directionalProjectile.speed = this.projectileSpeed;
        }
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
