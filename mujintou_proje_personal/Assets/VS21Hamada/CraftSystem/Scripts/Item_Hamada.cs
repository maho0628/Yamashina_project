using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHamada : ItemBase
{
    public ItemBaseStruct structItem;
    public int iItemID;
    public string Name, setumei;
    public bool isOverMouse, isGetItem;

    // Update is called once per frame
    void Update()
    {        
        if (isGetItem)
        {
            gameObject.transform.position =
                new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }

        if (isOverMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGetItem = !isGetItem;
            }
        }
    }
    private void OnMouseOver()
    {
        //isOverMouse = true;
    }
    private void OnMouseEnter()
    {
        isOverMouse = true;
    }
    private void OnMouseExit()
    {
        isOverMouse = false;
    }
    public int GetItemID()
    {
        return iItemID;
    }
}
