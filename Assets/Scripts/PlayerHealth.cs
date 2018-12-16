using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a singleton
public class PlayerHealth : MonoBehaviour
{
    //===========Singleton stuff===========
    public static PlayerHealth Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    //==========End singleton stuff==========

    public int hp = 3;
    public GameManager GM;
    
    public void ReduceHP(int amount)
    {
        hp -= amount;

        Debug.Log(hp);

        if(hp == 0)
        {
            GM.EndGame();
        }
    }
}
