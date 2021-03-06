﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Is a singleton
public class GameManager : MonoBehaviour
{
    private Vector3 bossSpawn = new Vector3(4, 0, 0);
    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject giftMenu;
    private GameObject bossInstance;
    public AudioSource music;
    public int currentBoss = 0; // The current boss' index
    private readonly string[] bosses = { "johnny_bravo", "centaur", "medusa" }; // Array with all the bosses' names 
                                                                                   //in order of progression
    private readonly float[] bossHPs = { 200, 200, 200 };   // The respective HP for each boss

    public PlayerHealth PlayerHealth;

    public BossHealth BossHealth;

    public void Start() {
        this.BossHealth = (BossHealth) FindObjectsOfType(typeof(BossHealth))[0];
        this.PlayerHealth = (PlayerHealth) FindObjectsOfType(typeof(PlayerHealth))[0];
        this.SpawnNextBoss();
    }

    public void EndGame()
    {
        gameOverScreen.SetActive(true);

        Destroy(player);
        Invoke("RestartGame", 4);
    }

    //Called by BossHealth when it's health reaches 0
    public void DefeatBoss()
    {
        currentBoss++;
        Destroy(bossInstance);

        GameObject[] leftOverProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        for(int i=0; i<leftOverProjectiles.Length; i++) {
            Destroy(leftOverProjectiles[i]);
        }

        // The game has finished
        if (currentBoss == 3)
        {
            currentBoss = 0;
            Invoke("WinGame", 4);
        }
        else
        {
            music.Stop();
            PlayerHealth.RegenerateHp(5);
            giftMenu.SetActive(true);
        }
        PlayerHealth.invulnerable = true;
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }

    public void RestartGame()
    {
        music.Stop();
        SceneManager.LoadScene(0);
    }

    public float GetCurrentBossHP()
    {
        return bossHPs[currentBoss];
    }
    private void OnEnable()
    {
        music.Play();
    }
    // Sets the next boss' health and spawns it
    public GameObject SpawnNextBoss()
    {
        music.Play();
        BossHealth.SetBossMaxHP(bossHPs[currentBoss]);
        bossInstance = (GameObject)Instantiate(Resources.Load(bosses[currentBoss]), bossSpawn, transform.rotation);
        return bossInstance;
    }
}
