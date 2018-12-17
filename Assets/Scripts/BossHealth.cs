using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float hp;    // Must be a float so as to display the health bar correctly
    public float maxHp;
    public GameObject bossHealthBar;

    //===========Singleton stuff===========
    public static BossHealth Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        BossHealth.onAwake.trigger();
    }

    public static Obs onAwake = new Obs();
    //==========End singleton stuff==========

    // Reduces the health by "amount"
    public void ReduceHealth(float amount)
    {
        hp -= amount;
        bossHealthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = hp / maxHp;

        if (hp <= 0)
        { 

            GameManager.instance.DefeatBoss();

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
