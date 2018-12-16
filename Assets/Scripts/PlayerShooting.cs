using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float fireRate;
    private float lastShot;

    void Start()
    {
        fireRate = 1;
    }

    void Update()
    {
        lastShot += Time.deltaTime;

        if (Input.GetKey("k")) {
            if(lastShot > fireRate)
            {
                Shoot();
                lastShot = 0;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }
}
