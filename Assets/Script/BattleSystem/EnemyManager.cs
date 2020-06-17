using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        ServiceLocator.Register<EnemyManager>(this);
        foreach (var enemy in GetComponentsInChildren<Enemy>())
        {
            enemies.Add(enemy);
        }
    }
}
