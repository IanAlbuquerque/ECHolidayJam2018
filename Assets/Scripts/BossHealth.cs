using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float hp;    // Must be a float so as to display the health bar correctly
    public float maxHp;
    public GameObject bossHealthBar;

    public GameManager GameManager;

    public void Start() {
        this.GameManager = (GameManager) FindObjectsOfType(typeof(GameManager))[0];
    }

    // Reduces the health by "amount"
    public AudioSource enemyHit;
    public void ReduceHealth(float amount)
    {
        enemyHit.Play();
        hp -= amount;
        bossHealthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = hp / maxHp;

        if (hp <= 0)
        { 
            GameManager.DefeatBoss();

            hp = maxHp;
        }
    }

    public void SetBossMaxHP(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        bossHealthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = hp / maxHp;
    }
}
