using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

    Node[,] allNodes;

    List<Node> openList;
    List<Node> closeList;

    Node begin;
    Node end;

    // Use this for initialization
    void Start () {
        Init();
    }

    void Init()
    {
        openList = new List<Node>();
        closeList = new List<Node>();
        allNodes = new Node[11, 11];
        for (int i = 0; i < allNodes.GetLength(0); i++)
        {
            for (int j = 0; j < allNodes.GetLength(1); j++)
            {
                //var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //go.transform.position = new Vector3(i - 5, 0, j - 5);
                //go.transform.SetParent(this.transform);
                //Node node = go.AddComponent<Node>();
                var go = transform.Find(i + "_" + j);
                Node node = go.GetComponent<Node>();
                //node.pos = new Vector2Int(i, j);

                allNodes[i, j] = node;
            }
        }

        // 起点和终点
        begin = allNodes[0, 0];
        begin.mType = NodeType.Start;
        openList.Add(begin);

        end = allNodes[10, 10];
        end.mType = NodeType.End;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartFindRoad();
        }
	}

    void StartFindRoad()
    {
        int num = 0; // 寻路次数
        // 中心点
        Node center;
        while (true)
        {
            num++ ;
            // 根据 F 排序
            openList.Sort((x, y) => x.F.CompareTo(y.F));
            // 排序后第0个就是最近的那个
            center = openList[0];
            
            //center.GetComponent<Renderer>().material.color = Color.red + new Color(-num/255f, num/255f, 0);
            if (center.mType == NodeType.End)
            {
                // 到达目的地， 寻路结束
                break;
            }

            // 获取上下左右的四个格子
            GetAround(center, -1, 0);
            GetAround(center, 1, 0);
            GetAround(center, 0, 1);
            GetAround(center, 0, -1);

            openList.Remove(center);
            closeList.Add(center);

            if (openList.Count == 0)
            {
                // 所有路都找遍了都没到目的地，寻路结束
                Debug.Log("Can't Arrive!");
                break;
            }
        }

        GetPath(center);
    }

    void GetPath(Node end)
    {
        List<Node> path = new List<Node>();
        Node cur = end;
        while (true)
        {
            path.Add(cur);
            if (cur.parent == null)
            {
                break;
            }
            cur = cur.parent;
        }

        Color offset = new Color(path.Count / 255f, -path.Count / 255f, 0, 0);
        for (int i = 0; i < path.Count; i++)
        {    
            path[i].GetComponent<Renderer>().material.color = Color.green + offset * i;
        }
        
    }

    void GetAround(Node center, int x, int y)
    {
        if (center.pos.x + x >= 0 &&
            center.pos.x + x < allNodes.GetLength(0) &&
            center.pos.y + y >= 0 &&
            center.pos.y + y < allNodes.GetLength(1))
        {
            Node target = allNodes[center.pos.x + x, center.pos.y + y];
            if (target.mType != NodeType.Obstacle && 
                !closeList.Contains(target) &&
                !openList.Contains(target))
            {
                target.G = center.G + 0;
                target.H = Mathf.Abs(target.pos.x - end.pos.x) + Mathf.Abs(target.pos.y - end.pos.y);
                target.F = target.G + target.H;

                target.parent = center;

                openList.Add(target);
            }
        }
    }
}
