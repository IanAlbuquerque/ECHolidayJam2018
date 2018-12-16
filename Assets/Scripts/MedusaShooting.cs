using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaShooting : MonoBehaviour
{
    public GameObject medusaShot;
    private float lastShot;
    public float shootingCooldown;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lastShot += Time.deltaTime;

        if (lastShot > this.shootingCooldown)
        {
            lastShot = 0;
            MedusaShoot();
        }
    }

    private void MedusaShoot()
    {
        GameObject shot;
        shot = Instantiate(medusaShot, transform.position + this.offset, transform.rotation);
        shot.GetComponent<Projectile>().SetShotPattern("Medusa");
    }
}
