using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHamada : MonoBehaviour
{
    [SerializeField] bool isOverMouse;
    [SerializeField] Sprite spNormal,spHilight;
    SpriteRenderer srRender;
    [SerializeField] Color cNormal,cClick;
    [SerializeField] UnityEvent ueEvents;
    void Start()
    {
        srRender = GetComponent<SpriteRenderer>();
        isOverMouse = false;
    }
    void Update()
    {
        if (isOverMouse)
        {
            srRender.sprite = spHilight;
        }
        else
        {
            srRender.sprite = spNormal;
        }
    }
    private void OnMouseDown()
    {
        srRender.color = cClick;
        ueEvents.Invoke();
    }
    private void OnMouseUp()
    {
        srRender.color = cNormal;
    }
    private void OnMouseEnter()
    {
        isOverMouse = true;
    }
    private void OnMouseExit()
    {
        isOverMouse = false;
    }
}
