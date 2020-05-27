using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickbleTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;


    private void OnMouseUp()
    {
        // check if Click UI
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // Debug.Log(tileX," ",tileY);
            map.MoveSelectedUnitTo(tileX, tileY);
        }
    }
}
