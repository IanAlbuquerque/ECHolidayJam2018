using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaShooting : MonoBehaviour
{
    public GameObject medusaShot;
    private float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lastShot += Time.deltaTime;

        if (lastShot > 0.5)
        {
            lastShot = 0;
            MedusaShoot();
        }
    }

    private void MedusaShoot()
    {
        GameObject shot;
        Vector3 offset = new Vector3(-1, 0, 0);
        shot = Instantiate(medusaShot, transform.position + offset, transform.rotation);

        shot.GetComponent<Projectile>().shotPattern = "Medusa";
    }
}
