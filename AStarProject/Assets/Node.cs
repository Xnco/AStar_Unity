using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Normal,
    Obstacle,
    Start,
    End
}

public class Node : MonoBehaviour {

    public NodeType mType;
    public Vector2Int pos;
    public Node parent;

    // 
    public int F;  // F = G+H
    public int G;  // 距离起始点的距离
    public int H;  // 距离终点的步数

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
