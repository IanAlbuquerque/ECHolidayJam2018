using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsObject;
    public GameObject tutorialObject;
    public GameObject heroObject;
    public AudioSource click;

    public void Start(){
        if(VictoryScreen.Instance){
            Destroy(VictoryScreen.Instance.gameObject);
            Credits();
        }
    }
    public void Play(){
        click.Play();
        SceneManager.LoadScene("MainGame");
    }

    public void Quit(){
        click.Play();
        Application.Quit();
    }

    public void Credits(){
        click.Play();
        gameObject.SetActive(false);
        this.heroObject.SetActive(false);
        this.tutorialObject.SetActive(false);
        this.creditsObject.SetActive(true);
    }
}
