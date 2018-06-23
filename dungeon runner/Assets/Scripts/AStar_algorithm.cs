using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar_algorithm : MonoBehaviour {
    // Attach to a transform that you
    // want to set a destination for
    
    // Node Things
    //public GameObject parentOfNodes;
    public GameObject Boss;
    public GameObject Player;
    public List<GameObject> nodes;
    public List<GameObject> OpenList;


    // Handles the Movement of the boss/enemy
    void Start ()
    {
        //for(int i = 0; i < parentOfNodes.transform.childCount; i++)
        //{
        //    nodes.Add(parentOfNodes.transform.GetChild(i).gameObject);
        //}

	}
	
    GameObject findNearNode(GameObject entity)
    {
        GameObject nearestNode;
        float shortestDist = Mathf.Infinity;
        int shortestNode = 0;

        for(int i = 0; i < nodes.Count; i++)
        {
            //print(nodes[i].transform.position);

            float dist = Vector3.Distance(entity.transform.position, nodes[i].transform.position);
                                    
            if(dist < shortestDist)
            {
                shortestNode = i;
                shortestDist = dist;
            }
        }
        nearestNode = nodes[shortestNode].gameObject;

        return nearestNode;
    }

    void fillOpenList(GameObject startingNode)
    {
        OpenList.Add(startingNode);
        startingNode.GetComponent<Node>().searched = true;

        // either find another attached node to go through the loop
        // Or if the last node in openlist == nearNode_player break recusion
        if(startingNode != findNearNode(Player))
        {
            float shortestDist = Mathf.Infinity;
            int shortestNode = 0;

            for (int i = 0; i < startingNode.GetComponent<Node>().AttachedNodes.Count; i++)
            {
                
                float dist = Vector3.Distance(Player.transform.position, startingNode.GetComponent<Node>().AttachedNodes[i].transform.position);

                // If Current node has been searched before
                if (startingNode.GetComponent<Node>().AttachedNodes[i].GetComponent<Node>().searched == true)
                {

                }


                if (dist <= shortestDist)
                {
                    shortestDist = dist;
                    shortestNode = i;
                }                
            }
            if(OpenList.Count<= 50)
            fillOpenList(startingNode.GetComponent<Node>().AttachedNodes[shortestNode].gameObject);
        }
    }

    void Update ()
    {
        // Start Node
        GameObject nearNode_boss = findNearNode(Boss);

        // End Node
        Transform nearNode_player = findNearNode(Player).transform;

        // Reset Searched Nodes
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].GetComponent<Node>().searched = false;
        }
        OpenList.Clear();


        fillOpenList(nearNode_boss);

        /*
         * Doesnt work because the last node has a possibility to never = player nearNode.
         * 
         */
        for (int i = 0; i < OpenList.Count-1; i++)
        {
            Debug.DrawLine(OpenList[i].transform.position, OpenList[i + 1].transform.position);
        }

        Boss.transform.position = nearNode_boss.transform.position;

	}
}