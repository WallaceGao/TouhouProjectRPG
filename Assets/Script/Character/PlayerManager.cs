using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;
    [SerializeField] GameObject _character3;

    public void Awake()
    {

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
}
