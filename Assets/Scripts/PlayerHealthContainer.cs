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

        for (int i = 0; i < PlayerHealth.Instance.hp ; i++)
        {
            Instantiate(heartPrefab, heartLayout.transform);
        } 

    }
    
    // Destroys the first heart in the indicator
    public void DestroyHeart()
    {
        Destroy(heartLayout.GetChild(heartLayout.transform.childCount - 1).gameObject);
    }
}
