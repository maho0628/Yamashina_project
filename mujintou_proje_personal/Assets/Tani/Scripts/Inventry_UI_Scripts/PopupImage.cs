using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Selectable))]
public class PopupImage : MonoBehaviour,IDeselectHandler
{
    Selectable selectable;
    public Slot spawned_slot = null;
    [SerializeField]
    Button ReturnButton;
    [SerializeField]
    Button PartitionButton;
    [SerializeField]
    Button DeleteButton;

    int normal_layer = 3;
    int popup_layer = 4;

    void Start()
    {
        selectable = GetComponent<Selectable>();
        selectable.Select();
        if(spawned_slot.Affiliation.GetSlotItem(spawned_slot.Slot_index).Value.amount <= 1)
        {
            PartitionButton.gameObject.SetActive(false);
        }
        ReturnButton.GetComponent<Image>().canvas.sortingOrder = popup_layer;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
        
        Destroy(gameObject, 0.2f);
        ReturnButton.GetComponent<Image>().canvas.sortingOrder = normal_layer;
    }

    public void RetutnButtonDown()
    {
        print("return");
    }

    public void PartitionButtonDown()
    {
        print("Partition");
        spawned_slot.Affiliation.SlotPartition(spawned_slot.Slot_index);
    }

    public void DeleteButtonDown()
    {
        print("Delete");
        spawned_slot.Affiliation.ClearSlot(spawned_slot.Slot_index);

    }
}
