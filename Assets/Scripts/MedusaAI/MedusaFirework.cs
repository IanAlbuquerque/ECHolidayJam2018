using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaFirework : MonoBehaviour, EnemyAttackPattern
{
    public GameObject fireworkBasePrefab;
    public GameObject directionalProjectilePrefab;
    public GameObject targetPrefab;

    private Vector2 startingPosition;

    public float baseSpeed = 1.0f;
    public float childSpeed = 1.0f;
    public float explosionDuration = 2.0f;
    public float explosionChildCooldown = 0.1f;
    public int numDirections = 10;

    private bool isRunning = false;
    public Vector2 standingPosition;
    public float speed = 1;

    [Header("Gizmos")]
    public float circleRadius = 0.2f;

    public float shootCooldown = 0.5f;
    private float shootCounter = 0.0f;

    private bool isStanding = false;


    public void startAttackPattern() {
        this.isRunning = true;
        this.shootCounter = 0.0f;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
    }

    //public AudioSource mShoot;
    private void shoot() {
        //mShoot.Play();
        Vector3 playerPosition3D = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector2 playerPosition2D = new Vector2(playerPosition3D.x, playerPosition3D.y);
        GameObject projectile = Instantiate(this.fireworkBasePrefab, this.transform.position, this.transform.rotation);
        FireworkBaseProjectile fireworkBaseProjectile = projectile.GetComponent<FireworkBaseProjectile>();
        fireworkBaseProjectile.setTarget(playerPosition2D, this.targetPrefab);
        fireworkBaseProjectile.childDirectionalProjectilePrefab = this.directionalProjectilePrefab;
        fireworkBaseProjectile.explosionChildCooldown = this.explosionChildCooldown;
        fireworkBaseProjectile.explosionDuration = this.explosionDuration;
        fireworkBaseProjectile.numDirections = this.numDirections;
        fireworkBaseProjectile.speed = this.baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isRunning) {
            if(!this.isStanding) {
                if(IsInWaypoint()) {
                    this.isStanding = true;
                }
                MoveTowardsWaypoint();
            } else {
                this.shootCounter += Time.deltaTime;
                if(this.shootCounter > this.shootCooldown) {
                    this.shootCounter = 0.0f;
                    this.shoot();
                }
            }
        }
    }

    public void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Vector3 pos3D = new Vector3(this.standingPosition.x, this.standingPosition.y, 0.0f);
        Gizmos.DrawSphere(pos3D, circleRadius);
    }

    void MoveTowardsWaypoint(){
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (this.standingPosition - pos2D).normalized*speed*Time.deltaTime;
        Vector3 dir3D = new Vector3(dir.x, dir.y, 0.0f);
        transform.Translate(dir3D);
    }

    bool IsInWaypoint(){
        if(transform.position.x < this.standingPosition.x - circleRadius){
            return false;
        }

        if(transform.position.x > this.standingPosition.x + circleRadius){
            return false;
        }

        if(transform.position.y < this.standingPosition.y - circleRadius){
            return false;
        }

        if(transform.position.y > this.standingPosition.y + circleRadius){
            return false;
        }

        return true;
    }
}
