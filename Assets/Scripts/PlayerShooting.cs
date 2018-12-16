using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float fireRate;
    private float lastShot;
    private GameObject shotInstance;
    public Vector3 shotOffset = new Vector3(0f, -0.1f, 0f);

    void Start()
    {
        
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
        
        shotInstance = Instantiate(arrowPrefab, transform.position + shotOffset, transform.rotation);

        shotInstance.GetComponent<Projectile>().SetShotPattern("Player");
    }
}
