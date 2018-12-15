using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simula o cenário levando os inimigos conforme vai sendo puxado
/// </summary>
public class BackgroundPush : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime);
    }
}
