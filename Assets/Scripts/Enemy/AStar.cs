using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public List<Vector3> AStarPathFinding(NodeChild start, NodeChild end)
    {
        if (start == null) return default;
        if (end == null) return default;
        Queue<NodeChild> frontier = new Queue<NodeChild>();
        frontier.Enqueue(start);
        
        var cameFrom = new Dictionary<NodeChild, NodeChild>();
        cameFrom.Add(start, null);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == end)
            {
                var path = new List<Vector3>();
                while (current != null)
                {
                    path.Add(current.transform.position);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }

            NodeChild closestNode = null;

            foreach (var item in current.connectedNodes)
            {
                if (cameFrom[current] == item) continue;                

                if (closestNode == null)
                    closestNode = item;
                else if (Heuristic(item.transform.position, end.transform.position) < Heuristic(closestNode.transform.position, end.transform.position))
                    closestNode = item;

                frontier.Enqueue(closestNode);
                cameFrom[closestNode] = current;
            }
        }
        return default;
    }

    float Heuristic(Vector3 start, Vector3 end)
    {
        return Vector3.Distance(start, end);
    }
}
