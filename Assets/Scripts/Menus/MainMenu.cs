using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("medusa");
    }

    public void Quit(){
        Application.Quit();
    }

    public void Credits(){
        gameObject.SetActive(false);
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
}
