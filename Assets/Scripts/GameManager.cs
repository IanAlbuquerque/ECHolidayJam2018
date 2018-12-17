using System.Collections;
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
    private int currentBoss = 0; // The current boss' index
    private readonly float[] bossHPs = { 100, 150, 100 };   // The respective HP for each boss
    private readonly string[] bosses = { "johnny_bravo", "centaur", "medusa" }; // Array with all the bosses' names 
                                                                                   //in order of progression

    //===========Singleton stuff===========
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;

        //bossInstance = (GameObject) Instantiate(Resources.Load(bosses[currentBoss]), bossSpawn, transform.rotation);
        BossHealth.onAwake.subscribe(() => {
            this.SpawnNextBoss();
        });
    }

    //===========End singleton stuff===========

    public void EndGame()
    {
        gameOverScreen.SetActive(true);

        Destroy(player);

        Invoke(SceneManager.GetActiveScene().ToString(), 4);
    }

    //Called by BossHealth when it's health reaches 0
    public void DefeatBoss()
    {
        currentBoss++;
        Destroy(bossInstance);

        // The game has finished
        if (currentBoss == 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            PlayerHealth.Instance.RegenerateHp(5);
            giftMenu.SetActive(true);
        }
    }


    public float GetCurrentBossHP()
    {
        return bossHPs[currentBoss];
    }

    // Sets the next boss' health and spawns it
    public GameObject SpawnNextBoss()
    {
        BossHealth.Instance.SetBossMaxHP(bossHPs[currentBoss]);
        bossInstance = (GameObject)Instantiate(Resources.Load(bosses[currentBoss]), bossSpawn, transform.rotation);
        return bossInstance;
    }
}
