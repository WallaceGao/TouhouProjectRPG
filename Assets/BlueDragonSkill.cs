using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragonSkill : Skill
{
    public BlueDragonSkill()
    {
        _name = "buleDragon";
        _range = 5;
        _damage = 0.0f;
        _cost = 0.0f;
    }

    public override void Ability()
    {
        Debug.Log("Attack");
    }
}
