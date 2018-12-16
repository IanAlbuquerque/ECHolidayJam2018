using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a singleton
public class PlayerHealth : MonoBehaviour
{
    public int hp;

    //===========Singleton stuff===========
    static private PlayerHealth PHInstance = null;
    private PlayerHealth()
    {
        hp = 3;
    }

    static public PlayerHealth GetPlayerHealth()
    {
        if(PHInstance == null)
        {
            PHInstance = new PlayerHealth();
        }

        return PHInstance;
    }
    //==========End singleton stuff==========
    
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
