using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaterPanel : PanelBase
{
    [SerializeField]
    Text water_value_text;
    [SerializeField]
    Text spring_water_text;
    [SerializeField]
    Button DrinkButton;
    [SerializeField]
    Button PourButton;
    [SerializeField]
    int waterAmountPerOnce = 10;
    bool invIsOn;

    //ウォーターパネルの汲むボダンのinteractable
    bool waterPanel_inteFalse;
    [SerializeField]
    PlayerInfo info;
    [SerializeField]
    GameObject InventoryMaxText;
    // Start is called before the first frame update
    protected override void Start()
    {
        InventoryMaxText.SetActive(false);

        info = PlayerInfo.Instance;
        SetSortOrder(OrderOfUI.NormalPanel);
        DrinkButton.onClick.AddListener(DrinkWater);
        PourButton.onClick.AddListener(PourWater);

        PourButton.interactable = PlayerInfo.Instance.Water >= 30 &&
                    PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) >= 1;
        DrinkButton.interactable = info.Water >= waterAmountPerOnce;
        
    }
    private void OnEnable()
    {
        //PourButton.interactable = PlayerInfo.Instance.Water >= 30 &&
        //                        PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) >= 1;
        //Debug.Log(PourButton.interactable);
        ItemMax();
        Debug.Log(PourButton.interactable);

    }

    protected override void Awake()
    {
        base.Awake();

    }


    protected override void Update()
    {
        if (PlayerInfo.InstanceNullable == null) return;
        int prev = info.Thirst;
        water_value_text.text = $"<b>{prev}% ⇒ {Mathf.Clamp(prev + waterAmountPerOnce, 0, 100)}%</b>";
        spring_water_text.text = $"貯水 : {info.Water}";

        DrinkButton.interactable = info.Water >= waterAmountPerOnce;
        Debug.Log(PourButton.interactable);
      ItemMax();

        //if ( PlayerInfo.Instance.Inventry.GetVisibility() == true )
        //{
        //    invIsOn = true;
        //}
        //if (invIsOn)
        //{
        //    if (PlayerInfo.Instance.Inventry.GetVisibility() == false)
        //    {
        //        Debug.Log(PourButton.interactable);

        //        invIsOn = false;
        //        Debug.Log(PourButton.interactable);

        //    }
        //}
        if (waterPanel_inteFalse == false && PlayerInfo.Instance.Water >= 30 &&
                                PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) >= 1)
        {
            PourButton.interactable = false;

        }
        else  if(waterPanel_inteFalse == true && PlayerInfo.Instance.Water >= 30&&
                                PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) >= 1)
            { 
            PourButton.interactable = true;

        }
        else if((waterPanel_inteFalse == true&& PlayerInfo.Instance.Water < 30 &&
                                PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) < 1))
        {
            PourButton.interactable = false;    
        }
        else
        {
            PourButton.interactable = false;

        }
    }

    void DrinkWater()
    {

        if(info.Thirst + waterAmountPerOnce >= 100)
        {
            info.Water -= 100 - info.Thirst;
            info.Thirst = 100;
        }
        else
        {
            info.Thirst += waterAmountPerOnce;
            info.Water -= waterAmountPerOnce;
        }
    }

    void PourWater()
    {
        PlayerInfo.Instance.Water -= 30;
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_bottle);
        PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_water2,1);
        
    }

    void ItemMax()
    {

        var slot = PlayerInfo.Instance.Inventry.GetNullSlot();
        if (slot == null)
        {
            Debug.Log("アイテムnull");
            waterPanel_inteFalse = false;
            InventoryMaxText.SetActive(true);
        }
        else if(slot != null)
        {
            Debug.Log("アイテムnotnull");
            Debug.Log(slot);
            waterPanel_inteFalse = true;
            InventoryMaxText.SetActive(false);  
            //PourButton.interactable = true;
        }
    }
    
}
