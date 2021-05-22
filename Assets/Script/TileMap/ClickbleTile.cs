using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickbleTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;
    private float lastClickTime;
    private const float doubleClickTime = 0.2f;

    private void OnMouseUp()
    {
        if (!map.selectUnit.GetComponent<Character>().GetIsMove)
        {
            // check if Click UI
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                // Debug.Log(tileX," ",tileY);
                map.MoveSelectedUnitTo(tileX, tileY);
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTime)
                {
                    map.selectUnit.GetComponent<Character>().StarMove();
                }
                lastClickTime = Time.time;
            }
        }
    }
}
