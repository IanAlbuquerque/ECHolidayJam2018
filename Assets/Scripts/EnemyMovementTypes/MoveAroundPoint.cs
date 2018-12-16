using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundPoint : MonoBehaviour
{
    public Vector3 point;
    public float radius = 3;
    public float angularSpeed = 1;
    float curAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();
        Vector3 next = CalculatePoint();
        
        transform.Translate(next - transform.position);
    }

    public void OnDrawGizmos(){
        //waypoints no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point, radius);
    }

    /// <summary>
    /// Calcula ângulo 
    /// </summary>
    void UpdateAngle(){
        curAngle += angularSpeed * Time.deltaTime;

        if(curAngle >= 2*Mathf.PI) curAngle -= 2*Mathf.PI;
    }

    /// <summary>
    /// Calcula o ponto para onde o inimigo vai
    /// </summary>
    /// <returns>ponto</returns>
    Vector3 CalculatePoint(){
        float x = point.x + radius*Mathf.Cos(curAngle);
        float y = point.y + radius*Mathf.Sin(curAngle);

        return new Vector3(x,y,transform.position.z);
    }
}
