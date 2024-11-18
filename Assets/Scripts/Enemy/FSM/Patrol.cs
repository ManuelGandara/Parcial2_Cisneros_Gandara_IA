using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    NodeChild[] waypoints;

    Enemy enemy;

    Vector3 dir;

    [SerializeField] float distToChange = 0.2f;
    
    public Patrol(Enemy en)
    {
        enemy = en;
    }

    public override void OnEnter()
    {
        waypoints = enemy.patrolWaypoints;
        enemy._pathToFollow.Clear();
        Debug.Log("entre en patrol");
    }

    public override void OnUpdate()
    {
        Movement();

        if (enemy.myFOV.InFieldOfView(GameManager.instance.player.transform.position))
        {
            if (EventManager.instance.pathfindingEndNode != enemy.currentNode)
                EventManager.instance.pathfindingEndNode = enemy.currentNode;

            EventManager.instance.alertOtherEnemies();
            //enemy._fsm.ChangeState(AgentStates.Chase);   Cambia el estado dentro del propio evento
        }
    }

    public override void OnExit()
    {
        Debug.Log("Salgo de patrol");
    }

    public void Movement()
    {
        dir = waypoints[enemy.waypointIndex].transform.position - enemy.gameObject.transform.position;

        if (dir.magnitude < distToChange)
        {
            //enemy.currentNode = waypoints[enemy.waypointIndex];
            if (enemy.waypointIndex < waypoints.Length - 1)
                enemy.waypointIndex++;
            else
                enemy.waypointIndex = 0;
        }
        enemy.Move(dir);
    }
}
