using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthContainer : MonoBehaviour
{
    public GameObject heartPrefab;
    private Transform heartLayout;

    // Start is called before the first frame update
    void Start()
    {
        heartLayout = transform.GetChild(1);
        SpawnHearts();
    }
    
    // Destroys the first heart in the indicator
    public void DestroyHeart()
    {
        Destroy(heartLayout.GetChild(heartLayout.transform.childCount - 1).gameObject);
    }

    public void DestroyHearts(){
        for (int i =  PlayerHealth.Instance.hp - 1; i >= 0; i--) Destroy(heartLayout.GetChild(i).gameObject);
    }

    public void SpawnHearts(){
        for (int i = 0; i < PlayerHealth.Instance.hp ; i++)
        {
            Instantiate(heartPrefab, heartLayout.transform);
        } 
    }
}
