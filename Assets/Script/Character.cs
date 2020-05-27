using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //character's ability
    [SerializeField]
    string name;
    public int tileX;
    public int tileY;
    public List<Node> currentPath = null;
    public TileMap map;

    int level { get; set; }
    int experience { get; set; }
    int hp { get; set; }
    bool isDead { get; set; }

    [SerializeField]
    Race.Type type;

    [SerializeField]
    int maxExperience = 0;

    [SerializeField]
    int maxHp = 0;

    [SerializeField]
    int speed = 0;

    [SerializeField]
    int attack = 0;

    [SerializeField]
    int defence = 0;

    [SerializeField]
    string BackGround;


    private void Awake()
    {
        level = 1;
        hp = maxHp;
        experience = maxExperience;
        isDead = false;
        AbilitySetting();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (experience >= maxExperience)
        {
            level++;
            AbilitySetting();
        }

        if (currentPath != null)
        {
            int currNode = 0;
            while (currNode < currentPath.Count-1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0,0.1f,0);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode+1].x, currentPath[currNode+1].y) + new Vector3(0, 0.1f, 0);
                Debug.DrawLine(start, end, Color.red);
                currNode++;
            }
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // draw street line from the camera's mouse position to where I click
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    // save the obect that i click on
        //    RaycastHit hit = new RaycastHit();
        //    // out is path by reference
        //    if (Physics.Raycast(ray,out hit))
        //    {
        //        GetComponent<Pathfinding.AIDestinationSetter>().target = hit.transform; 
        //    }
        //}

        if(Input.GetKeyUp(KeyCode.Space))
        {
            MoveNextTile();
            transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord(tileX, tileY), 1f * Time.deltaTime);
        }
    }

    public void MoveNextTile()
    {
        float remainingMovement = speed;
        while(remainingMovement > 0)
        {
            if (currentPath == null)
            {
                return;
            }
            remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);
            
            //update the character position
            tileX = currentPath[1].x;
            tileY = currentPath[1].y;
            //now grab the new first node and move ise to that position
            transform.position = map.TileCoordToWorldCoord(tileX, tileY);
            
            //Move animation not work
            //while (transform.position.x - map.TileCoordToWorldCoord(tileX, tileY).x < 0f && transform.position.z - map.TileCoordToWorldCoord(tileX, tileY).z < 0f)
            //{
            //    transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord( tileX, tileY ), 1f * Time.deltaTime);
            //}
            //remove the old current/first node from the path
            currentPath.RemoveAt(0);
            if (currentPath.Count == 1)
            {
                // we only have one tile left in the path, and that tile must be our ultimate destination, 
                // clear the pathfinding 
                currentPath = null;
            }
        }
    }

    private void AbilitySetting()
    {
        maxHp = Convert.ToInt32(maxHp + (level * maxHp));
        attack = Convert.ToInt32(attack + (level * 0.4f * attack));
        defence = Convert.ToInt32(defence + (level * 0.4f * defence));
        speed = Convert.ToInt32(speed + (level * 0.4f * speed));
        maxExperience = Convert.ToInt32(maxExperience + (level * 1.2f * maxExperience));
    }
}
