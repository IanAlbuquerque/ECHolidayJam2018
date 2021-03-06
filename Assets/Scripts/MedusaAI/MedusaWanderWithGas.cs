﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaWanderWithGas : MonoBehaviour, EnemyAttackPattern
{
    public GameObject gasProjectilePrefab;
    public GameObject directionalProjectilePrefab;
    private bool isRunning = false;
    public List<Vector2> waypoints;
    public float speed = 1;

    [Header("Gizmos")]
    public float circleRadius = 0.2f;

    int curWaypointIdx = 0;

    public float shootCooldown = 0.5f;
    public float projectileSpeed = 1.0f;
    private float shootCounter = 0.0f;

    public float gasCooldown = 5.0f;
    public float gasSpeed = 0.1f;
    private float gasCounter = 0.0f;


    public void startAttackPattern() {
        this.isRunning = true;
        this.shootCounter = 0.0f;
        this.gasCounter = 0.0f;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
    }

    public AudioSource mShoot;
    private void shoot() {
        mShoot.Play();
        Vector3 playerPosition3D = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector2 playerPosition2D = new Vector2(playerPosition3D.x, playerPosition3D.y);
        Vector2 enemyPosition2D = new Vector2(this.transform.position.x, this.transform.position.y);
        GameObject projectile = Instantiate(this.directionalProjectilePrefab, this.transform.position, this.transform.rotation);
        DirectionalProjectile directionalProjectile = projectile.GetComponent<DirectionalProjectile>();
        directionalProjectile.direction = playerPosition2D - enemyPosition2D;
        directionalProjectile.speed = this.projectileSpeed;
    }

    public AudioSource mGas;
    private void shootGas() {
        //mGas.Play();
        GameObject projectile = Instantiate(this.gasProjectilePrefab, this.transform.position, this.transform.rotation);
        DirectionalProjectile gasProjectile = projectile.GetComponent<DirectionalProjectile>();
        gasProjectile.direction = new Vector2(-1.0f, 0.0f);
        gasProjectile.speed = this.gasSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isRunning) {
            if(IsInWaypoint()) {
                IncrementIdx();
            }
            MoveTowardsWaypoint();
            this.shootCounter += Time.deltaTime;
            this.gasCounter += Time.deltaTime;
            if(this.shootCounter > this.shootCooldown) {
                this.shootCounter = 0.0f;
                this.shoot();
            }
            if(this.gasCounter > this.gasCooldown) {
                this.gasCounter = 0.0f;
                this.shootGas();
            }
        }
    }

    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        if(waypoints == null) return;
        foreach(Vector2 waypoint in waypoints){
            Vector3 pos3D = new Vector3(waypoint.x, waypoint.y, 0.0f);
            Gizmos.DrawSphere(pos3D, circleRadius);
        }
    }

    void MoveTowardsWaypoint(){
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (waypoints[curWaypointIdx] - pos2D).normalized*speed*Time.deltaTime;
        Vector3 dir3D = new Vector3(dir.x, dir.y, 0.0f);
        transform.Translate(dir3D);
    }

    bool IsInWaypoint(){
        Vector2 curWaypointPos = waypoints[curWaypointIdx];

        if(transform.position.x < curWaypointPos.x - circleRadius){
            return false;
        }

        if(transform.position.x > curWaypointPos.x + circleRadius){
            return false;
        }

        if(transform.position.y < curWaypointPos.y - circleRadius){
            return false;
        }

        if(transform.position.y > curWaypointPos.y + circleRadius){
            return false;
        }

        return true;
    }

    void IncrementIdx(){
        if(curWaypointIdx < waypoints.Count - 1){
            curWaypointIdx++;
        } else {
            curWaypointIdx = 0;
        }
    }
}
