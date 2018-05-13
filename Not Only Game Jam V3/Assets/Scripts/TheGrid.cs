using System.Collections.Generic;
using UnityEngine;
using System;

public class TheGrid : MonoBehaviour
{

    public int m_size_x;
    public int m_size_y;
    public int m_nodeSize;


    private int currentMinF;
    private int currentMinH;


    public CNode[,] grid;
    public List<CNode> portals = new List<CNode>();

    private void Awake()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid = new CNode[m_size_x, m_size_y];

        for (int i = 0; i < m_size_x; i++)
        {
            for (int j = 0; j < m_size_y; j++)
            {

                Vector3 nodePosition = new Vector3(i * m_nodeSize + 0.5f * m_nodeSize, j * m_nodeSize + 0.5f * m_nodeSize, 0); // multiplicar es bien dividir es mal
                Vector3 worldPosition = nodePosition + transform.position;
                Collider[] colliders = Physics.OverlapSphere(worldPosition, m_nodeSize * 0.5f);

                bool isTransitable = true;
                for (int k = 0; k < colliders.Length; k++)
                {
                    if (colliders[k].tag == "Wall")
                        isTransitable = false;



                }


                grid[i, j] = new CNode(i, j, m_nodeSize, worldPosition, isTransitable);
                //grid[i, j].Grita();

            }
        }
    }

    public CNode GetNodeContainingPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - transform.position;

        int x = Mathf.FloorToInt(localPosition.x / m_nodeSize);
        int y = (int)localPosition.y / m_nodeSize;


        if (x < m_size_x && x >= 0 && x < m_size_y && y >= 0)
        {
            return grid[x, y];
        }
        return null;
    }
    public CNode GetNode(int x, int y)
    {
        if (x < 0 || y < 0 || x >= m_size_x || y >= m_size_y)
        {
            Debug.LogWarning("Se ha pedido un nodo FUERA de la grid con posicion : " + x + ", " + y);

            return null;
        }


        return grid[x, y];
    }




    

    public CNode GetRandomNode()
    {
        List<CNode> TransitableNodes = new List<CNode>();
        System.Random alea = new System.Random();

        foreach (CNode n in grid)
        {
            if (n.isTransitable)
                TransitableNodes.Add(n);
        }

        if (TransitableNodes != null)
            return TransitableNodes[alea.Next(0, TransitableNodes.Count)];
        else
            return null;


    }

   

    private void OnDrawGizmosSelected()
    {

        if (grid != null)
        {

            Gizmos.color = Color.green;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Vector3 position = new Vector3(i*nodeSize + 0.5f*nodeSize, j*nodeSize + 0.5f*nodeSize, 0); // multiplicar es bien dividir es mal
                    Vector3 scale = new Vector3(m_nodeSize, m_nodeSize, m_nodeSize);
                    if (!grid[i, j].isTransitable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(grid[i, j].position, scale);
                        Gizmos.color = Color.green;


                    }
                    else
                    {
                        Gizmos.DrawWireCube(grid[i, j].position, scale);

                    }


                }
            }
        }
    }


}