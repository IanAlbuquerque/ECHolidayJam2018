using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMovementAroundPoint : MonoBehaviour
{
    public Vector3 circleCenter;
    Vector3 curSpiralCenter;
    public float circleRadius = 3;
    public float spiralRadius = 0.5f;
    public float circleSpeed = 10;
    public float spiralSpeed = 1;
    int curCircleWaypointIdx = 0;
    float curSpiralAngle = 0;

    bool movingToWaypoint = false;

    List<Vector3> waypoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        if(circleSpeed != 0) CreateCircleWaypoints();
        curSpiralCenter = waypoints[curCircleWaypointIdx];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 next;

        if(movingToWaypoint){
            Debug.Log("C");
            if(IsInWaypoint()){
                Debug.Log("D");
                movingToWaypoint = false;
                return;
            }

            next = MoveToWaypoint();
        }

        else{
            UpdateSpiralAngle();
            next = CalculateNextSpiralPoint() - transform.position;
        }

        transform.Translate(next);
    }

    public void OnDrawGizmos(){
        //waypoints no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, circleRadius);

        List<Vector3> v = new List<Vector3>();
        if(circleSpeed != 0) CreateCircleWaypoints(v);

        foreach(Vector3 p in v) Gizmos.DrawWireSphere(p, spiralRadius);

    }

    void CreateCircleWaypoints(List<Vector3> v){
        for(float i = 0; i <= 2*Mathf.PI; i += circleSpeed){
            v.Add(CalculateCirclePoint(i));
        }
    }

    void CreateCircleWaypoints(){
        CreateCircleWaypoints(waypoints);
    }

    /// <summary>
    /// Calcula ângulo do próximo ponto da espiral
    /// </summary>
    void UpdateSpiralAngle(){
        curSpiralAngle += spiralSpeed * Time.deltaTime;

        if(curSpiralAngle >= 2*Mathf.PI){
            movingToWaypoint = true;
            curSpiralAngle = 0;
            IncrementIdx();
            curSpiralCenter = waypoints[curCircleWaypointIdx];
        }
    }

    /// <summary>
    /// Calcula o próximo ponto da espiral
    /// </summary>
    /// <returns>ponto</returns>
    Vector3 CalculateNextSpiralPoint(){
        float x = curSpiralCenter.x + spiralRadius*Mathf.Cos(curSpiralAngle);
        float y = curSpiralCenter.y + spiralRadius*Mathf.Sin(curSpiralAngle);

        return new Vector3(x,y,transform.position.z);
    }

    /// <summary>
    /// Calcula o ponto do círculo
    /// </summary>
    /// <returns>ponto</returns>
    Vector3 CalculateCirclePoint(float angle){
        float x = circleCenter.x + circleRadius*Mathf.Cos(angle);
        float y = circleCenter.y + circleRadius*Mathf.Sin(angle);

        return new Vector3(x,y,transform.position.z);
    }

    Vector3 MoveToWaypoint(){
        Debug.Log("B");
        Vector3 dir = (waypoints[curCircleWaypointIdx] - transform.position).normalized;

        return dir*spiralSpeed*Time.deltaTime;
    }

    /// <summary>
    /// Checa se inimigo está dentro do waypoint
    /// </summary>
    /// <returns>true se sim, false se não</returns>
    bool IsInWaypoint(){
        Vector3 curWaypointPos = waypoints[curCircleWaypointIdx];

        if(transform.position.x < curWaypointPos.x - spiralRadius*Mathf.Cos(curSpiralAngle)){
            return false;
        }

        if(transform.position.x > curWaypointPos.x + spiralRadius*Mathf.Cos(curSpiralAngle)){
            return false;
        }

        if(transform.position.y < curWaypointPos.y - spiralRadius*Mathf.Sin(curSpiralAngle)){
            return false;
        }

        if(transform.position.y > curWaypointPos.y + spiralRadius*Mathf.Sin(curSpiralAngle)){
            return false;
        }

        return true;
    }

    /// <summary>
    /// Muda o waypoint para o próximo
    /// </summary>
    void IncrementIdx(){
        if(curCircleWaypointIdx < waypoints.Count - 1){
            curCircleWaypointIdx++;
        }

        else curCircleWaypointIdx = 0;
    }
}
