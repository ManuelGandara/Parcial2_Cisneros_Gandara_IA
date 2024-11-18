using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingState : State
{
    Enemy enemy;

    public TravelingState(Enemy en)
    {
        enemy = en;
    }

    public override void OnUpdate()
    {


        if (enemy.myFOV.InFieldOfView(GameManager.instance.player.transform.position))
        {
           enemy._fsm.ChangeState(AgentStates.Chase);
           return;
        }

        if (enemy._pathToFollow == null || enemy._pathToFollow.Count <= 0)
        {
           if(enemy.myFOV.InFieldOfView(enemy.patrolWaypoints[enemy.waypointIndex].transform.position))
               enemy._fsm.ChangeState(AgentStates.Patrol);
           else
               enemy._pathToFollow = enemy.aStar.AStarPathFinding(enemy.currentNode, enemy.patrolWaypoints[enemy.waypointIndex]); 
            //Debug.Log("Finn del viaje");
        }
        else
            TravelThroughPath();

    }

    public override void OnEnter()
    {
        //enemy._pathToFollow = enemy.aStar.AStarPathFinding(enemy.currentNode, EventManager.instance.pathfindingEndNode);
        Debug.Log("entre en travel");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de travel");
    }

    void TravelThroughPath()
    {
        if (enemy._pathToFollow == null || enemy._pathToFollow.Count <= 0) return;

        //enemy.testChase(enemy._pathToFollow);     ==> Testeo Propio

        Vector3 posTarget = enemy._pathToFollow[0];  // ==> Codigo de la clase
        //posTarget.z = enemy.transform.position.z;
        Vector3 dir = posTarget - enemy.transform.position;
        enemy.transform.forward = dir;
        if (dir.magnitude < 0.05f)
        {
            //SetPosition(posTarget);
            enemy._pathToFollow.RemoveAt(0);
        }

        enemy.Move(dir);
    }

    void SetPosition(Vector3 pos)
    {
        pos.z = enemy.transform.position.z;
        enemy.transform.position = pos;
    } 
}
