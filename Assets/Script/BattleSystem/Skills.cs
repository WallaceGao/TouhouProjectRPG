using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill
{
    //public event System.Action fly;
    [SerializeField] protected string _name;
    [SerializeField] protected int _range;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _cost;
    public string Name { get { return _name; } }

    public abstract void Ability();

}
