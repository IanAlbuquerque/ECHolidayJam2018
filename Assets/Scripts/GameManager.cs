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
    private int currentBoss = 0; // The current boss' index
    private readonly float[] bossHPs = {/* 50, 75, */100 };   // The respective HP for each boss
    private readonly string[] bosses = {/*"johnny_bravo", "centaur", */"medusa"}; // Array with all the bosses' names 
                                                                              //in order of progression
    
    //===========Singleton stuff===========
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    //===========End singleton stuff===========

    public void EndGame()
    {
        gameOverScreen.SetActive(true);

        Destroy(player);

        Invoke(SceneManager.GetActiveScene().ToString(), 4);
    }
    
    public float GetCurrentBossHP()
    {
        return bossHPs[currentBoss];
    }

    public GameObject GetNextBoss()
    { 
        currentBoss++;

        return (GameObject) Instantiate(Resources.Load(bosses[currentBoss]), bossSpawn, transform.rotation);
    }
}
