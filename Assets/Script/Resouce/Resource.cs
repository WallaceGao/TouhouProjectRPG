using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ResourceType
{
    Stone, Wood
}

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceType _resourceType;
    [SerializeField] int _Amount;
    [SerializeField] int _totalHealth;
    [SerializeField] int _currentHealth;

    private void Awake()
    {
        _currentHealth = _totalHealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Destroy(this);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage; 
    }
}
