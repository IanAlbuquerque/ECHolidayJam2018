using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public List<Vector3> waypoints;
    public float speed = 1;

    [Header("Gizmos")]
    public float circleRadius = 1f;

    int curWaypointIdx = 0; //guarda indice do waypoint atual na lista

    // Start is called before the first frame update
    void Start()
    {
        waypoints.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInWaypoint()){
            IncrementIdx();
        }

        MoveTowardsWaypoint();
    }

    public void OnDrawGizmos(){
        //waypoints no editor
        Gizmos.color = Color.red;

        if(waypoints == null) return;
        foreach(Vector3 waypoint in waypoints){
                Gizmos.DrawSphere(waypoint, circleRadius);
        }
    }

    /// <summary>
    /// Se move em direção ao waypoint atual
    /// </summary>
    void MoveTowardsWaypoint(){
        Vector3 dir = (waypoints[curWaypointIdx] - transform.position).normalized;

        transform.Translate(dir*speed*Time.deltaTime);
    }

    /// <summary>
    /// Checa se inimigo está dentro do waypoint
    /// </summary>
    /// <returns>true se sim, false se não</returns>
    bool IsInWaypoint(){
        Vector3 curWaypointPos = waypoints[curWaypointIdx];

        if(transform.position.x < curWaypointPos.x - circleRadius){
            return false;
        }

        if(transform.position.x > curWaypointPos.x + circleRadius){
            return false;
        }

        if(transform.position.y < curWaypointPos.y - circleRadius){
            return false;
        }

        if(transform.position.y > curWaypointPos.y + circleRadius){
            return false;
        }

        return true;
    }

    /// <summary>
    /// Muda o waypoint para o próximo
    /// </summary>
    void IncrementIdx(){
        if(curWaypointIdx < waypoints.Count - 1){
            curWaypointIdx++;
        }

        else curWaypointIdx = 0;
    }
}
