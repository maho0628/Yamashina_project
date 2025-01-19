using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class OPSkipSystem : MonoBehaviour
{
    [SerializeField]
    GameObject Skip_display_prefab;


    [SerializeField]
    SceneObject baseLocation;
    [SerializeField]
    Button SkipButton;
    void Start()
    {

    }
    public void DestroySkipButton()
    {
        if (SkipButton)
        {
            Destroy(SkipButton.gameObject);
        }
    }
    
    public void CreatSkipMenu()
    {
        var go =  Instantiate<GameObject>(Skip_display_prefab);
        foreach (var button in go.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() =>
            {
                var data = button.gameObject.GetComponent<SpecialItemData>();
                if (data)
                {
                    int itemNum;
                    if(data.specil_item_id == Items.Item_ID.item_special_food) 
                         { itemNum = 5; }
                    else { itemNum = 1; }
                    PlayerInfo.Instance.Inventry.GetItem(data.specil_item_id, itemNum);
                    PlayerInfo.Instance.FirstItemId = (int)data.specil_item_id;
                    PlayerInfo.Instance.DoAction();
                    PlayerInfo.Instance.DoAction();
                    //危険//ここから先、激ヤバコード//危険//
                    //PlayerInfo.Instance.Inventry.SetVisible(true);

                    //SlotManager.selectedItem = (PlayerInfo.Instance.Inventry.GetComponent<SlotManager>(),0);
                    //SlotManager.selectedItem.slotManager.UseSlotItem(SlotManager.selectedItem.index);
                    //GameObject.FindAnyObjectByType<DetailPanel>().gameObject.transform.GetChild(4).GetComponent<Button>().onClick.Invoke();

                    //PlayerInfo.Instance.Inventry.SetVisible(false);
                    //危険//ここまで激ヤバコード//危険//
                    PlayerInfo.Instance.MaxActionValue++;

                    PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1);

                    PlayerInfo.Instance.Health = 100;
                    PlayerInfo.Instance.Thirst = 83;
                    PlayerInfo.Instance.Hunger = 75;
                    SceneManager.LoadScene(baseLocation);
                }
                else
                {
                    Destroy(go);

                }

            });
        }
    }
}
