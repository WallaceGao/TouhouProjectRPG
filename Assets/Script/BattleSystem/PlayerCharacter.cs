using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{

    public override void Awake()
    {
        base.Awake();
        level = 1;
        experience = maxExperience;
        AbilitySetting();
    }

    public override void Update()
    {
        base.Update();

        if (experience >= maxExperience)
        {
            level++;
            AbilitySetting();
        }

        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // draw street line from the camera's mouse position to where I click
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    // save the obect that i click on
        //    RaycastHit hit = new RaycastHit();
        //    // out is path by reference
        //    if (Physics.Raycast(ray,out hit))
        //    {
        //        GetComponent<Pathfinding.AIDestinationSetter>().target = hit.transform; 
        //    }
        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //MoveNextTile();
            //transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord(tileX, tileY), 1f * Time.deltaTime);
            // How to use ServicLocator
            //ServiceLocator.Get<UIManager>().DisplaySkill();
        }
    }

    private void AbilitySetting()
    {
        maxHp = Convert.ToInt32(maxHp + (level * maxHp));
        attack = Convert.ToInt32(attack + (level * 0.4f * attack));
        defence = Convert.ToInt32(defence + (level * 0.4f * defence));
        speed = Convert.ToInt32(speed + (level * 0.4f * speed));
        maxExperience = Convert.ToInt32(maxExperience + (level * 1.2f * maxExperience));
    }
}
