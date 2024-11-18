using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AgentStates
{
    Patrol,
    Chase,
    Traveling,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5; 

    public FiniteStateMachine _fsm;

    public NodeChild[] patrolWaypoints;
    public int waypointIndex = 0;

    [SerializeField] public NodeChild currentNode;
    public AStar aStar = new AStar();

                                                        //FOV    
    public FoV myFOV;

    public List<Vector3> _pathToFollow = new List<Vector3>();

    private void Awake()
    {
                                                        //FSM
        _fsm = new FiniteStateMachine();
                                                        //FOV
        myFOV = GetComponent<FoV>();
    }

    void Start()
    {
                                                        //FSM
        State Patrol = new Patrol(this);
        State Chase = new Chase(this);
        State Travel = new TravelingState(this);

        _fsm.AddState(AgentStates.Patrol, Patrol);
        _fsm.AddState(AgentStates.Chase, Chase);
        _fsm.AddState(AgentStates.Traveling, Travel);

        _fsm.ChangeState(AgentStates.Patrol);
        EventManager.instance.alertOtherEnemies += FoundPlayer;
        //EventManager.instance.observers.Add(this);
    }

    void Update()
    {
        _fsm.Update();
    }

    public void Move(Vector3 dir)
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.forward = dir;
    }    

    public void FoundPlayer()
    {
        Debug.Log("Soy " + this.gameObject.name + " y quiero triggeriar el Evento");

        if (myFOV.InFieldOfView(GameManager.instance.player.transform.position))
        {
            _fsm.ChangeState(AgentStates.Chase);
        }
        else
        {
            if(_pathToFollow.Count <= 0)
            {
                _pathToFollow = aStar.AStarPathFinding(currentNode, EventManager.instance.pathfindingEndNode);
                _fsm.ChangeState(AgentStates.Traveling);
            }
        }
        
        //Debug.Log("Soy " + this.gameObject.name + " y pude triggeriar el Evento");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            currentNode = other.gameObject.GetComponent<NodeChild>();
        }
    }

    /*public IEnumerator Notify(NodeChild whereIsHe)
    {
        Debug.Log("Soy " + this.gameObject.name + " y quiero triggeriar el Evento");

        if (myFOV.InFieldOfView(GameManager.instance.player.transform.position))
        {
            EventManager.instance.pathfindingEndNode = whereIsHe;
            _fsm.ChangeState(AgentStates.Chase);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _pathToFollow = aStar.AStarPathFinding(currentNode, EventManager.instance.pathfindingEndNode);
            _fsm.ChangeState(AgentStates.Traveling);
        }

        Debug.Log("Soy " + this.gameObject.name + " y pude triggeriar el Evento");
    }*/
}
