using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject hoveredObject;
    [SerializeField] private TileMap mTileMap;

    // Update is called once per frame
    void Update()
    {
        // get the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObejct = hitInfo.transform.root.gameObject;
            SelectObject(hitObejct);
        }
        else
        {
            ClearSelection();
        }

        
        if (Input.GetMouseButtonDown(0) && hoveredObject.tag == "PlayerCharacter")
        {
            selectedObject = hoveredObject;
            mTileMap.SetSelectUnit(selectedObject);
        }
    }

    void SelectObject(GameObject obj)
    {
        if(hoveredObject != null)
        {
            if (obj == hoveredObject)
            {
                return;
            }
            ClearSelection();
        }

        hoveredObject = obj;
        //TODO Highlight Object
        //Get All Children
        //Renderer[] rs = hoveredObject.GetComponentsInChildren<Renderer>();
        //foreach (Renderer r in rs)
        //{
        //    Material material = r.material;
        //    material.color = Color.red;
        //    r.material = material;
        //}
    }

    void ClearSelection()
    {
        hoveredObject = null;
    }



    //void MouseClick()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        print("implement when object is selected");
    //        RaycastHit hitInfo = new RaycastHit();
    //        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
    //
    //        if (hit)
    //        {
    //            Debug.Log($"The object that was selected is: {hitInfo.transform.gameObject.name}");
    //
    //            if (hitInfo.transform.gameObject.name == "blueDragon")
    //            {
    //                print("do something here");
    //            }
    //        }
    //    }
    //}
}
