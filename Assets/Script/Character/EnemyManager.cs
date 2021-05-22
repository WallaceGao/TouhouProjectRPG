using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private TileMap tileMap;
    [SerializeField] private GameObject soundManager;

    private void Awake()
    {
        Transform location = transform;
        foreach (var enemy in enemies)
        {
            Instantiate(enemy, location.position, Quaternion.identity);
            enemy.GetComponent<Character>().map.Equals(tileMap);
            enemy.GetComponent<Character>().soundManager.Equals(soundManager);
            enemy.GetComponent<Character>().SetStartTileX = 1;
            enemy.GetComponent<Character>().SetStartTileY = 3;
            enemy.GetComponent<Character>().Awake();
        }
    }


    private void Update()
    {
         
    }

    private void CharacterDie(int index)
    {

    }

    private void CharacterMove()
    {

    }

    private void Attack()
    {

    }
}
