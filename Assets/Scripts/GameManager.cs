using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    private PlayerHealth HP;
    public void EndGame()
    {
        gameOverScreen.SetActive(true);
        
        HP = PlayerHealth.GetPlayerHealth();

        Destroy(HP.gameObject);
    }
}
