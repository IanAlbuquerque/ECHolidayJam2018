﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurChargeWithoutAura : MonoBehaviour, EnemyAttackPattern
{

    private Vector2 startingPosition;



    private bool isRunning = false;
    public Vector2 standingPosition;
    public float speed = 1;
    public float chargeSpeed = 1;

    [Header("Gizmos")]
    public float circleRadius = 0.2f;

    private bool isStanding = false;

    private bool isIdle = false;

    public float idleDuration = 2.0f;

    private float idleCounter = 0.0f;

    private bool hasSpawnedAxes = false;

    public float boomerangCooldown = 1.0f;

    private float boomerangCounter = 0.0f;
    public float boomerangSpeed = 1.0f;

    public float boomerantOrthogonalSpeed = 0.5f;

    public float boomerangAcceleration = -0.5f;

    public GameObject boomerangGameObject;

    private List<AxeProjectile> axes = new List<AxeProjectile>();

    public void startAttackPattern() {
        this.isRunning = true;
        this.isStanding = false;
        this.isIdle = false;
        this.idleCounter = 0.0f;
        this.hasSpawnedAxes = false;
        this.boomerangCounter = 0.0f;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
        for(int i=0; i<this.axes.Count; i++) {
            Destroy(this.axes[i].gameObject);
        }
        this.axes = new List<AxeProjectile>();
    }

    void followPlayer() {
        Vector3 playerPosition3D = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector3 playerPosition3DCorrected = new Vector3(playerPosition3D.x, playerPosition3D.y, 0.0f);
        Vector3 enemyPosition3DCorrected = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
        Vector3 direction = playerPosition3DCorrected - enemyPosition3DCorrected;
        direction.Normalize();
        if(direction.x >= 0) {
            direction.x *= 2.0f;
        }
        this.transform.Translate(direction * this.chargeSpeed * Time.deltaTime);
    }

    void shootBoomerang() {
        Vector3 playerPosition3D = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector2 playerPosition2D = new Vector2(playerPosition3D.x, playerPosition3D.y);
        Vector2 enemyPosition2D = new Vector2(this.transform.position.x, this.transform.position.y);
        GameObject projectile = Instantiate(this.boomerangGameObject, this.transform.position, this.transform.rotation);
        DirectionalBoomerangProjectile directionalBoomerangProjectile = projectile.GetComponent<DirectionalBoomerangProjectile>();
        directionalBoomerangProjectile.direction = playerPosition2D - enemyPosition2D;
        directionalBoomerangProjectile.speed = this.boomerangSpeed;
        directionalBoomerangProjectile.orthogonalSpeed = this.boomerantOrthogonalSpeed;
        directionalBoomerangProjectile.acceleration = this.boomerangAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isRunning) {
            if(!this.isStanding) {
                if(IsInWaypoint()) {
                    this.isStanding = true;
                    this.idleCounter = 0.0f;
                    this.isIdle = true;
                }
                MoveTowardsWaypoint();
            } else {
                if(this.isIdle == true) {
                    this.idleCounter += Time.deltaTime;
                    if(this.idleCounter > this.idleDuration) {
                        this.isIdle = false;
                    }
                } else {
                    this.boomerangCounter += Time.deltaTime;
                    if(this.boomerangCounter >= this.boomerangCooldown) {
                        this.shootBoomerang();
                        this.boomerangCounter = 0.0f;
                    }
                    this.followPlayer();
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
