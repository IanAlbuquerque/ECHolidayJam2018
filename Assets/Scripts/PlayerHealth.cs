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

    public int hp;
    public int maxHp;
    public bool invulnerable = false;
    public PlayerHealthContainer hpContainer;
    
    public void ReduceHP(int amount)
    {
        if(!invulnerable)
        {
            hp -= amount;

            hpContainer.DestroyHeart();

            if(hp <= 0)
            {
                Debug.Log(hp);
                GameManager.instance.EndGame();
            }
            invulnerable = true;
        }
    }

    private float timer = 0;
    private void Update()
    {
        if(invulnerable && timer <= 2)
        {
            timer += Time.deltaTime;
        }
        else
        {
            invulnerable = false;
            timer = 0;
        }
    }

    // Regenerates "regen" points to the hp
    public void RegenerateHp(int regen)
    {
        hpContainer.DestroyHearts();

        int newHP = hp+regen;

        if (newHP > maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp = newHP;
        }

        hpContainer.SpawnHearts();
    }
}
