using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [SerializeField] public NodeChild pathfindingEndNode;

    //public Action<NodeChild> alertOtherEnemies;

    public delegate void AlertOtherEnemies();

    public AlertOtherEnemies alertOtherEnemies;
 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
