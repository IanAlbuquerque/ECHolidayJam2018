using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //===========Singleton stuff===========
    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    //==========End singleton stuff==========
    private Rigidbody2D rigidbody2D;
    public float movementSpeed = 2.0f;
    public float movementIncrease = 50f;
    private Vector2 currentSpeed = new Vector2(0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 goalSpeed = direction * this.movementSpeed * Time.deltaTime; 
        rigidbody2D.velocity = goalSpeed;
    }

    public void IncreaseSpeed(){
        movementSpeed += movementIncrease;
    }

    public void DecreaseSpeed(){
        movementSpeed -= movementIncrease;
    }
}
