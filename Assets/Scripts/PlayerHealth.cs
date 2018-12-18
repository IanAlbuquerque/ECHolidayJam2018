using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a singleton
public class PlayerHealth : MonoBehaviour
{
    public GameManager GameManager;

    public int hp;
    public int maxHp;
    public bool invulnerable = false;
    public PlayerHealthContainer hpContainer;

    public AudioSource playerHit;

    public void Start() {
        this.GameManager = (GameManager) FindObjectsOfType(typeof(GameManager))[0];
    }
    
    public void ReduceHP(int amount)
    {
        if(!invulnerable)
        {
            playerHit.Play();
            hp -= amount;

            hpContainer.DestroyHeart();

            if(hp <= 0)
            {
                GameManager.EndGame();
            }
            invulnerable = true;
        }
    }

    private float timer = 0;
    private void Update()
    {
        if(invulnerable && timer <= 2)
        {
            GetComponent<Animator>().SetLayerWeight(1, 1f);
            timer += Time.deltaTime;
        }
        else
        {
            invulnerable = false;
            timer = 0;
            GetComponent<Animator>().SetLayerWeight(1, 0f);
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
