using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    List<Character> enemies = new List<Character>();

    private void Awake()
    {
        ServiceLocator.Register<EnemyManager>(this);
        foreach (var enemy in GetComponentsInChildren<Character>())
        {
            enemies.Add(enemy);
        }
    }
}
