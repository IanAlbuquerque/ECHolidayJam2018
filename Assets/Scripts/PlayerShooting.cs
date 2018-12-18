using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float fireRate;
    private float lastShot;
    private GameObject shotInstance;
    public Vector3 offset;
    public float damage = 1;

    //===========Singleton stuff===========
    public static PlayerShooting instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    //===========End singleton stuff===========

    void Update()
    {
        lastShot += Time.deltaTime;

        if (Input.GetKey("space")) {
            if(lastShot > fireRate)
            {
                Shoot();
                lastShot = 0;

                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void Shoot()
    { 
        shotInstance = Instantiate(arrowPrefab, transform.position + this.offset, transform.rotation);
    }
}
