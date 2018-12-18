using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject creditsObject;
    public GameObject tutoralObject;
    public GameObject menuObject;
    public GameObject heroObject;

    public void Back(){
        this.menuObject.SetActive(true);
        this.heroObject.SetActive(true);
        this.tutoralObject.SetActive(true);
        this.creditsObject.SetActive(false);
    }
}
