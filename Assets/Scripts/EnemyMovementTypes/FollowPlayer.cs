using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 1;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //Procura referência do jogador
        GameObject playerObj = GameObject.Find("Player");

        if(playerObj == null) Debug.LogError("Player não encontrado");

        player = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
