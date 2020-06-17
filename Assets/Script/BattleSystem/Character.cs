using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //character's ability
    [SerializeField]
    protected string name;
    public int tileX;
    public int tileY;
    public List<Node> currentPath = null;
    public TileMap map;

    protected int level { get; set; }
    protected int experience { get; set; }
    protected int hp { get; set; }
    protected bool isDead { get; set; }
    protected List<Skills> skills = new List<Skills>();
    public List<Skills> Skills { get { return skills; } }
    [SerializeField]
    protected GameObject skillHolder;

    [SerializeField]
    protected Race.Type type;

    [SerializeField]
    protected int maxExperience = 0;

    [SerializeField]
    protected int maxHp = 0;

    [SerializeField]
    protected int speed = 0;

    [SerializeField]
    protected int attack = 0;

    [SerializeField]
    protected int defence = 0;

    [SerializeField]
    protected string BackGround;

    public virtual void Awake() 
    {
        hp = maxHp;
        isDead = false;
        foreach (var skill in skillHolder.GetComponentsInChildren<Skills>())
        {
            skills.Add(skill);
        }
    }

    public virtual void Update()
    {
        if (isDead)
        {
            return;
        }

        if (currentPath != null)
        {
            int currNode = 0;
            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0, 0.1f, 0);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + new Vector3(0, 0.1f, 0);
                Debug.DrawLine(start, end, Color.red);
                currNode++;
            }
        }
    }

    public void MoveNextTile()
    {
        float remainingMovement = speed;
        while (remainingMovement > 0)
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

}
