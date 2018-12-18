using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthContainer : MonoBehaviour
{
    public GameObject heartPrefab;
    private Transform heartLayout;

    public PlayerHealth PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        this.PlayerHealth = (PlayerHealth) FindObjectsOfType(typeof(PlayerHealth))[0];
        heartLayout = transform.GetChild(1);
        SpawnHearts();
    }
    
    // Destroys the first heart in the indicator
    public void DestroyHeart()
    {
        Destroy(heartLayout.GetChild(heartLayout.transform.childCount - 1).gameObject);
    }

    public void DestroyHearts(){
        for (int i =  PlayerHealth.hp - 1; i >= 0; i--) Destroy(heartLayout.GetChild(i).gameObject);
    }

    public void SpawnHearts(){
        for (int i = 0; i < PlayerHealth.hp ; i++)
        {
            Instantiate(heartPrefab, heartLayout.transform);
        } 
    }
}
