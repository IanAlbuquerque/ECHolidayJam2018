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

    void Update()
    {
        lastShot += Time.deltaTime;

        if (Input.GetKey("space") || Input.GetKey("k") || Input.GetKey("return")) {
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
