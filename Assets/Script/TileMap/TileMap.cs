using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    public GameObject selctedUnit;
    //List<Node> currentPath = null;
    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;
    [SerializeField] 
    int mapSizeX = 5;
    [SerializeField]
    int mapSizeY = 10;
    

    private void Start()
    {
        //Setup the selectedUnit
        selctedUnit.GetComponent<Character>().tileX = (int)selctedUnit.transform.position.x;
        selctedUnit.GetComponent<Character>().tileY = (int)selctedUnit.transform.position.y;
        selctedUnit.GetComponent<Character>().map = this;

        //Allocate our map tiles
        tiles = new int[mapSizeX, mapSizeY];

        GeneratePathfindingGraph();

        //Initialize our map tiles
        for (int x = 0; x < mapSizeX; ++x)
        {
            for (int y = 0; y < mapSizeY; ++y)
            {
                tiles[x, y] = 0;
            }
        }

        //test edge
        tiles[0, 1] = 1;
        tiles[1, 2] = 1;

        tiles[4, 2] = 1;
        tiles[4, 3] = 1;
        tiles[4, 4] = 1;
        tiles[4, 5] = 1;
        tiles[4, 6] = 1;
        tiles[4, 7] = 1;

        tiles[2, 3] = 1;
        tiles[2, 4] = 1;
        tiles[1, 7] = 1;
        tiles[1, 8] = 1;
        tiles[1, 9] = 1;

        //visual prefabs
        GenerateMapVisual();
    }

    public float CostToEnterTile( int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];
        if(!UnitCanEnterTile(targetX,targetY))
        {
            return Mathf.Infinity;
        }
        float cost = tt.movementCost;
        if (sourceX  != targetX && sourceY != targetY)
        {
            // we are move diagonally
            // purely a cosmetic thing
            cost += 0.001f;
        }
        return cost;
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; ++x)
        {
            for (int y = 0; y < mapSizeY; ++y)
            {
                GameObject go;
                TileType tt = tileTypes[tiles[x, y]];
                if ( y%2 == 0)
                {
                    go = Instantiate(tt.tileVisualPrefab, new Vector3(x + x * 2.5f, 0.01f, y), Quaternion.identity);
                    go.transform.parent = transform; 
                }
                else
                {
                    go = Instantiate(tt.tileVisualPrefab, new Vector3(x + x * 2.5f +1.75f, 0.01f,(y-1f) +1f), Quaternion.identity); 
                    go.transform.parent = transform;
                }
                ClickbleTile ct = go.GetComponent<ClickbleTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        if (y % 2 == 0)
        {
            return new Vector3(x + x * 2.5f, 0.01f, y);
        }
        else
        {
            return new Vector3(x + x * 2.5f + 1.75f, 0.01f, (y - 1f) + 1f);
        }
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        //we could test the character with fly
        return tileTypes[tiles[x,y]].isWalkble;
    }

    public void MoveSelectedUnitTo(int x, int y)
    {
        //selctedUnit.GetComponent<Character>().tileX = x;
        //selctedUnit.GetComponent<Character>().tileY = y;
        //if (y % 2 == 0)
        //{
        //    selctedUnit.transform.position = TileCoordToWorldCoord(x,y);
        //}
        //else
        //{
        //    selctedUnit.transform.position = TileCoordToWorldCoord(x, y);
        //}
        //clear the path
        
        selctedUnit.GetComponent<Character>().currentPath = null;

        if(UnitCanEnterTile(x,y) == false)
        {
            //just quit out
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        List<Node> unvisted = new List<Node>();
        // serup the "Q"
        Node source = graph[selctedUnit.GetComponent<Character>().tileX, selctedUnit.GetComponent<Character>().tileY];
        Node target = graph[x, y];
        dist[source] = 0;
        prev[source] = null;
        // Initialize everying to have distance
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisted.Add(v);
        }
        while (unvisted.Count > 0)
        {
            // u is goint to be the unvisted node with the smallest distance
            Node u = null;
            foreach (Node possibleU in unvisted)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
                break;

            unvisted.Remove(u);
            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (dist[target] == null)
        {
            //no route between our target 
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node curr = target;

        //set through the "prev" chain and add it to curretpath
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }
        currentPath.Reverse();
        selctedUnit.GetComponent<Character>().currentPath = currentPath;
    }

    void GeneratePathfindingGraph()
    {
        // Initialize the array
        graph = new Node[mapSizeX, mapSizeY];
        // Initializer each spot in the array
        for (int x = 0; x < mapSizeX; ++x)
        {
            for (int y = 0; y < mapSizeY; ++y)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }
        // all the nodes exist, calculate their neighbours
        for (int x = 0; x < mapSizeX; ++x)
        {
            for (int y = 0; y < mapSizeY; ++y)
            {
                ////6 way connected map
                //// left up
                if (y > 0)
                {
                    //even
                    if (y % 2 == 1 && x < mapSizeX - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    }
                    //odd
                    else if(y % 2 == 0)
                    {
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    }
                }
                ////left down
                if (y < mapSizeY - 1 )
                {
                    //even
                    if (y % 2 == 1 && x < mapSizeX - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                    }
                    //odd
                    else if(y % 2 == 0)
                    {
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                    }
                }
                //// right up
                if (y > 0)
                {
                    //even
                    if (y % 2 == 1)
                    {
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    }
                    //odd
                    else if (y % 2 == 0 && x > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    }
                }
                //// right down
                if (y < mapSizeY - 1 )
                {
                    //even
                    if (y % 2 == 1)
                    {
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                    }
                    //odd
                    else if(y % 2 == 0 && x > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                    }
                }
                //// up
                if (y > 1)
                    graph[x, y].neighbours.Add(graph[x, y - 2]);
                // down
                if (y < mapSizeY - 2)
                    graph[x, y].neighbours.Add(graph[x, y + 2]);
            }
        }
    }
}
