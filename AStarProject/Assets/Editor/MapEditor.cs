using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapWindow:EditorWindow {
    
    [MenuItem("Tools/打开地图编辑器")]
    public static void OpenWindow()
    {
        MapWindow window = GetWindow<MapWindow>(true, "地图编辑器");
    }

    //Vector2Int pos;

    private void OnGUI()
    {
        if (GUILayout.Button("创建地图"))
        {
            GameObject map = new GameObject("Map");
            map.AddComponent<AStar>();

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.name = i + "_" + j;
                    go.transform.position = new Vector3(i - 5, 0, j - 5);
                    go.transform.SetParent(map.transform);
                    Node node = go.AddComponent<Node>();
                    node.pos = new Vector2Int(i, j);
                }
            }
        }

        //pos = EditorGUILayout.Vector2IntField(new GUIContent("坐标"), pos);

        if (Selection.activeGameObject != null)
        {
            if (GUILayout.Button("设置为起点"))
            {
                Selection.activeGameObject.GetComponent<Node>().mType = NodeType.Start;
                Selection.activeGameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            if (GUILayout.Button("设置为墙"))
            {
                Selection.activeGameObject.GetComponent<Node>().mType = NodeType.Obstacle;
                Selection.activeGameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            if (GUILayout.Button("设置为路"))
            {
                Selection.activeGameObject.GetComponent<Node>().mType = NodeType.Normal;
                Selection.activeGameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            if (GUILayout.Button("设置为终点"))
            {
                Selection.activeGameObject.GetComponent<Node>().mType = NodeType.End;
                Selection.activeGameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}
