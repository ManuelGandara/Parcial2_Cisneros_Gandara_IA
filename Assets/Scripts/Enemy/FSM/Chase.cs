using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Enemy enemy;
    Vector3 _velocity;
    float _maxForce= 5;

    public Chase(Enemy en)
    {
        enemy = en;
    }
    public override void OnEnter()
    {
        //enemy._pathToFollow.Clear();
        Debug.Log("entre en chase");
    }

    public override void OnExit()
    {
        /*Vector3 dist = Vector3.zero;
        Vector3 X;

        foreach (var item in NodeArray.father.nodeList)
        {
            X = item.transform.position - enemy.transform.position;
            if (dist == Vector3.zero) dist = X;
            if (dist.sqrMagnitude > X.sqrMagnitude)
            {
                dist = X;
                enemy.currentNode = item;
            }
                   
        }*/
        Debug.Log("sali de chase");
    }

    public override void OnUpdate()
    {
        Move();
        AddForce(Seek());

        if (!enemy.myFOV.InFieldOfView(GameManager.instance.player.transform.position))
        {


                enemy._fsm.ChangeState(AgentStates.Traveling);
        }

        if(Vector3.Distance(enemy.transform.position, GameManager.instance.player.transform.position) <= 0.35f) 
            GameManager.instance.player.gameObject.SetActive(false);
    }

    public void Move()
    {
        enemy.transform.position += _velocity * Time.deltaTime;
        enemy.transform.forward = _velocity;
        if (_velocity != Vector3.zero) enemy.transform.forward = _velocity;
    }

    Vector3 Seek()
    {
        Vector3 desired = GameManager.instance.player.transform.position - enemy.transform.position;
        return desired.normalized;
    }

    void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxForce);
    }
}
