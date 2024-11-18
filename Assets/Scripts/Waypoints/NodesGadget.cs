using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesGadget : MonoBehaviour
{
    [SerializeField] float size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, size);
    }
}