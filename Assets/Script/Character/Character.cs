using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //character's ability
    BlueDragonSkill _skill;

    [SerializeField] private string name;
    [SerializeField] private int startTileX = 0;
    public int SetStartTileX { set {startTileX = value; } }
    [SerializeField] private int startTileY = 0;
    public int SetStartTileY { set {startTileY = value; } }
    public int tileX;
    public int tileY;
    public List<Node> currentPath = null;
    public TileMap map;
    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _rotaSpeed = 0;
    [SerializeField] AnimationStateController animation;
    [SerializeField] private string WalkSound;
    private bool isDead { get; set; }
    //private List<Skills> skills = new List<Skills>();
    //public List<Skills> Skills { get { return skills; } }
    [SerializeField] private int hp;
    [SerializeField] private GameObject skillHolder;
    [SerializeField] private Race.Type type;
    [SerializeField] private int maxHp = 0;
    [SerializeField] private int speed = 0;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defence = 0;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AudioClip footstep;
    public GameObject soundManager;
    private AudioSource audioSourceManager;

    private Vector3 _targetDirection;
    private bool _isMove = false;
    public bool GetIsMove { get { return _isMove; } }
    public bool SetIsMove { set { _isMove = value; } }
    private float remainingMovement = 0.0f;
    private bool _isFinishAction = false;
    public bool GetFinishAction { get { return _isFinishAction; } }
    public bool SetFinishAction { set { _isFinishAction = value; } }

    [SerializeField] private bool _CanGetResouce;
    public bool GetResouce { get { return _CanGetResouce; } }

    public void Awake() 
    {
        _skill = new BlueDragonSkill(); 

        audioSourceManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        audioSourceManager.clip = footstep;

        hp = maxHp;
        isDead = false;
        //foreach (var skill in skillHolder.GetComponentsInChildren<Skills>())
        //{
        //    skills.Add(skill);
        //}
        _isMove = false;
        SetStarPosition(startTileX, startTileY);

        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);
    }

    public void Update()
    {
        if (isDead)
        {
            Destroy(this);
        }

        if (_isFinishAction)
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
            audioSourceManager.Play();
            //FindObjectOfType<SoundManager>().Play(WalkSound);
            MoveNextTile();
        }
        else
        {
            audioSourceManager.Stop();
            //FindObjectOfType<SoundManager>().Stop(WalkSound);
        }

        if (_targetDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_targetDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(5);
        }
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
                //Finish move
                if (remainingMovement <= 0)
                {
                    remainingMovement = 0;
                    _isMove = false;
                    animation.SetIdle();
                    _isFinishAction = true;
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
                _isFinishAction = true;
            }
        }
    }

    public void SetStarPosition(int starX, int starY)
    {
        //now grab the new first node and move ise to that position
        tileX = starX;
        tileY = starY;
        transform.position = map.TileCoordToWorldCoord(starX, starY);
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

    public void takeDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage; 
        }
        if (hp <= 0)
        {
            isDead = true;
        }
    }

    public void Attack()
    {
        _skill.Ability();
    }

}
