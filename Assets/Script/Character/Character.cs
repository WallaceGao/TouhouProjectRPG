using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //character's ability
    [SerializeField] private string name;
    public int tileX;
    public int tileY;
    public List<Node> currentPath = null;
    public TileMap map;
    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _rotaSpeed = 0;
    [SerializeField] AnimationStateController animation;
    [SerializeField] private string WalkSound;
    private int hp { get; set; }
    private bool isDead { get; set; }
    private List<Skills> skills = new List<Skills>();
    public List<Skills> Skills { get { return skills; } }
    [SerializeField] private GameObject skillHolder;
    [SerializeField] private Race.Type type;
    [SerializeField] private int maxHp = 0;
    [SerializeField] private int speed = 0;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defence = 0;

    [SerializeField] private AudioClip footstep;
    [SerializeField] private GameObject soundManager;
     
    private Vector3 _targetDirection;
    private bool _isMove = false;
    public bool GetIsMove { get { return _isMove; } }
    private float remainingMovement = 0.0f;

    public virtual void Awake() 
    {
        soundManager.GetComponent<AudioSource>().clip = footstep;

        hp = maxHp;
        isDead = false;
        foreach (var skill in skillHolder.GetComponentsInChildren<Skills>())
        {
            skills.Add(skill);
        }
        _isMove = false;
    }

    private void Start()
    {
           
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

        if (_isMove)
        {
            //FindObjectOfType<SoundManager>().Play(WalkSound);
            MoveNextTile();
        }
        else
        {
            //FindObjectOfType<SoundManager>().Stop(WalkSound);
        }

        transform.rotation = Quaternion.LookRotation(_targetDirection);
        
    }

    public void MoveNextTile()
    {
        if (remainingMovement > 0)
        {
            if (currentPath == null)
            {
                return;
            }

            //update the character position
            tileX = currentPath[1].x;
            tileY = currentPath[1].y;
            //now grab the new first node and move ise to that position
            //transform.position = map.TileCoordToWorldCoord(tileX, tileY);

            //Move animation not work
            Vector3 targetPosistion = map.TileCoordToWorldCoord(tileX, tileY);
            _targetDirection = targetPosistion - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosistion, _moveSpeed * Time.deltaTime);

            if (transform.position.x == map.TileCoordToWorldCoord(tileX, tileY).x && transform.position.z == map.TileCoordToWorldCoord(tileX, tileY).z)
            {
                remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);
                currentPath.RemoveAt(0);
                if (remainingMovement <= 0)
                {
                    remainingMovement = 0;
                    _isMove = false;
                    animation.SetIdle();
                }
            }

            //remove the old current/first node from the path
            if (currentPath.Count == 1)
            {
                // we only have one tile left in the path, and that tile must be our ultimate destination, 
                // clear the pathfinding 
                currentPath = null;
                remainingMovement = 0;
                _isMove = false;
                animation.SetIdle();
            }
        }
    }

    public void StarMove()
    {
        if (!_isMove)
        {
            _isMove = true;
            remainingMovement = speed;
            animation.SetRun();
        }
    }

}
