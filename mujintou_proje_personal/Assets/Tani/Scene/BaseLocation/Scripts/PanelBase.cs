using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelBase : MonoBehaviour
{

    [SerializeField]
    Canvas ownedMainCanvas;

    protected UnityEvent<bool> OnStateChange = new UnityEvent<bool>();
    

    virtual protected void Awake()
    {
        
    }

    virtual protected void Start()
    {
        
    }


    virtual protected void Update()
    {
        
    }

    virtual protected void Init() { }

    public void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
        OnStateChange.Invoke(enabled);
    }

    public void SwitchEnabaled()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        OnStateChange.Invoke(gameObject.activeSelf);
    }

    protected void SetSortOrder(OrderOfUI order)
    {
        ownedMainCanvas.overrideSorting = true;
        ownedMainCanvas.sortingOrder = (int)order;
    }

    protected enum OrderOfUI
    {
        MainPanel,
        NormalPanel,
        Inventry,
        PlayerStatus,
        Option,
        CraftPanel = 7,
    }
}
