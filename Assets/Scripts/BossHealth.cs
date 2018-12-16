using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float hp;    // Must be a float so as to display the health bar correctly
    public float maxHp;
    public GameObject bossHealthBar;
    public GameObject currentBoss;

    //===========Singleton stuff===========
    public static BossHealth Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    //==========End singleton stuff==========

    // Reduces the health by "amount"
    public void ReduceHealth(float amount)
    {
        hp -= amount;
        Debug.Log(hp / maxHp);
        bossHealthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = hp / maxHp;

        if (hp <= 0)
        {
            Destroy(currentBoss);
            /*
            Chama a tela de presentes             
            */
            /*PlayerHealth.Instance.RegenerateHp(5);
            GameManager.instance.GetNextBoss();
            maxHp = GameManager.instance.GetCurrentBossHP();

            hp = maxHp;*/
        }
    }


}
