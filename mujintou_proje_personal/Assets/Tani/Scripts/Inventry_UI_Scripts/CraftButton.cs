using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CraftButton : MonoBehaviour
{
    [SerializeField]
    CraftSlots craft;

    Button button;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (craft.UseActionValue)
        {
            button.interactable = craft.CheckCraftable() != 0 && PlayerInfo.Instance.ActionValue >= 1;
        }
        else
        {
            button.interactable = craft.CheckCraftable() != 0;
        }
        
    }
}
