using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeArray : MonoBehaviour
{
    public List<NodeChild> nodeList = new List<NodeChild>();
    [SerializeField] float childNum;

    public static NodeArray father;

    private void Awake()
    {
        father = this;
        childNum = transform.childCount;        
        MakeList();
    }

    void MakeList() 
    {
        for (int i = 0; i < childNum; i++)
        {
            nodeList.Add(transform.GetChild(i).gameObject.GetComponent<NodeChild>());
        }
    }
}
