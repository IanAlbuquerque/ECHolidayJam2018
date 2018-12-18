using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenBoss : MonoBehaviour
{
    public bool equal = false;
    public bool under = false;
    public int value = 0;

    public GameManager GameManager;

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        this.GameManager = (GameManager) FindObjectsOfType(typeof(GameManager))[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(this.equal) {
            if(GameManager.currentBoss == this.value) {
                this.obj.SetActive(true);
            } else {
                this.obj.SetActive(false);
            }
        } else {
            if(this.under) {
                if(GameManager.currentBoss < this.value) {
                    this.obj.SetActive(true);
                } else {
                    this.obj.SetActive(false);
                }
            } else {
                if(GameManager.currentBoss >= this.value) {
                    this.obj.SetActive(true);
                } else {
                    this.obj.SetActive(false);
                }
            }
        }
    }
}
