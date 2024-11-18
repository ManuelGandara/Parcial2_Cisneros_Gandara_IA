using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFather : MonoBehaviour
{
    [SerializeField] protected float magnitudFoV;
    [SerializeField] public List<NodeChild> connectedNodes = new List<NodeChild>();
    [SerializeField] LayerMask layerMask;
    RaycastHit hit;

    protected void ConnectNodes(Vector3 nodePos)
    {
        for (int i = 0; i < NodeArray.father.nodeList.Count; i++)
        {
            if(!InLineOfSight(NodeArray.father.nodeList[i].transform.position))
                continue;
            if(NodeArray.father.nodeList[i].gameObject != this.gameObject)
                connectedNodes.Add(NodeArray.father.nodeList[i]);
            
        }
        RemoveFarNodes();
    }

    protected bool InLineOfSight(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        return !Physics.Raycast(transform.position, dir, dir.magnitude, layerMask);
    }

    protected void RemoveFarNodes()
    {
        for (int i = 0; i< connectedNodes.Count; i++)
        {
            Vector3 asd = connectedNodes[i].transform.position - transform.position;
            if (asd.magnitude > magnitudFoV)
            {
                connectedNodes.Remove(connectedNodes[i]);
                i--;
            }
            else
            {
                continue;
            }
        }
    }

   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnitudFoV);
    }
}