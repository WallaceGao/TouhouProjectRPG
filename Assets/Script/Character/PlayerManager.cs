using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;
    [SerializeField] GameObject _character3;
    [SerializeField] int _Wood;
    [SerializeField] int _Stone;

    public void Awake()
    {
        _Wood = 0;
        _Stone = 0;
    }

    public void Update()
    {
        
    }

    public void FinishTurn()
    {
        _character1.GetComponent<Character>().SetFinishAction = false;
        _character2.GetComponent<Character>().SetFinishAction = false;
        _character3.GetComponent<Character>().SetFinishAction = false;
    }

    public void GetWood(int value)
    {
        _Wood += value; 
    }

    public void GetStone(int value)
    {
        _Stone += value;
    }
}
