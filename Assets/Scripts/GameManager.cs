using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Is a singleton
public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject player;
    
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

        //Invoke(SceneManager.GetActiveScene(), 4);
    }
}
