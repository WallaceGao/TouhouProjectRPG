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
    [SerializeField] PlayerManager _playerManager;

    private void Awake()
    {
        _currentHealth = _totalHealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            if (_resourceType == ResourceType.Stone)
            {
                _playerManager.GetStone(_Amount);
            }
            else
            {
                _playerManager.GetWood(_Amount);
            }

            Destroy(this);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage; 
    }
}
