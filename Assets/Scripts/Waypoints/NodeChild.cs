using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeChild : NodeFather
{
    Dictionary<NodeChild, List<NodeChild>> nodeAndNeighbours = new Dictionary<NodeChild, List<NodeChild>>();

    public void Start()
    {
       ConnectNodes(transform.position);
    }

    private void OnDrawGizmos()
    {
        if (connectedNodes == null || connectedNodes.Count == 0) return;
        foreach(var item in connectedNodes)
        {
            Gizmos.DrawLine(transform.position, item.transform.position);
        }
    }
}
