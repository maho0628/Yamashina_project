using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightEventControll2 : MonoBehaviour
{
    public TextControl textComponent;
    public float interval = 1f; 

    [SerializeField] Items.Item_ID[] Gain_Items;
    float probability = 1;
    int getNum;
    // Start is called before the first frame update
    void Start()
    {
        bool stilDest = false;
        getNum = 0;
        probability = 0.45f;
        string result_text = string.Empty;

        foreach (var n in Gain_Items)
        {
            Debug.Log("foreach");
            // ���̃A�C�e�����擾�ł��邩
            bool bGet = Random.value <= probability;
            if (bGet)
            {
                Debug.Log(probability);
                //�����l���ł��邩
                int num = 1;
                if (PlayerInfo.Instance.Inventry.GetNullSlot())
                {
                    probability -= 0.09f;
                    getNum++;
                    result_text += PlayerInfo.Instance.Inventry.GetItemName(n) + $"��{num}�l�����܂����B\n";
                    PlayerInfo.Instance.Inventry.GetItem(n, num);
                }
                else
                {
                    result_text += PlayerInfo.Instance.Inventry.GetItemName(n) + $"{num}�͎����؂�Ȃ�����!\n";
                }
            }
            
        }
        if (getNum == 0)
        {
            result_text += PlayerInfo.Instance.Inventry.GetItemName(Items.Item_ID.item_mat_shell) + $"��{1}�l�����܂����B\n";
            PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_shell, 1);
        }

        PlayerInfo.Instance.SavePalyerData();
        textComponent.AddTextData(result_text);
        Debug.Log(result_text);

    }

}

