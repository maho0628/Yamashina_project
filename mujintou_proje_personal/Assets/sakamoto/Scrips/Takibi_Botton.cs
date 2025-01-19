using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takibi_Botton : MonoBehaviour
{
    //選択肢を隠すやつ
    [SerializeField] GameObject shadow1;
    [SerializeField] GameObject shadow2;
    [SerializeField] GameObject shadow3;

    //条件となるアイテムのプレハブ
    [SerializeField] NewItem item1;
    [SerializeField] NewItem item2;
    [SerializeField] NewItem item3;

    //各条件の確率
    [SerializeField] public  static int kakuritu1;
    [SerializeField]  public static int kakuritu2;
    [SerializeField] public static int kakuritu3;

    //各火おこしの体力減少値

    int[] kakuritu_tum;
    int Rondam_num_tum;
    // Start is called before the first frame update

    public void setShadow()
    {
        //if(!(item1.CurrentStackCount > 0))
        //{
        //    shadow1.SetActive(true);
        //}
        if(!(item2.CurrentStackCount > 0))
        {
            shadow2.SetActive(true);
        }
        if(!(item3.CurrentStackCount > 0))
        {
            shadow3.SetActive(true);
        }
    }

    //ボタン1の処理
    public void botton1()
    {
        kakuritu_tum = new int[10];
        for (int i = 0; i < 10; i++)
        {
            if(i < kakuritu1)
            {
                kakuritu_tum[i] = 1;
            }
            else
            {
                kakuritu_tum[i] = 0;
            }
        }
        Rondam_num_tum = (int)(Random.Range(0.1f, 2.0f) - 0.1f);
        if(Rondam_num_tum == 1)
        {
            Debug.Log("点火");
        }
        if(Rondam_num_tum == 0)
        {
            Debug.Log("点火失敗");
        }
        PlayerInfo.Instance.Health -= 15;
        PlayerInfo.Instance.Hunger -= 15;
        PlayerInfo.Instance.Thirst -= 15;
    }

    //ボタン2の処理
    public void botton2()
    {
        kakuritu_tum = new int[10];
        for (int i = 0; i < 10; i++)
        {
            if(i < kakuritu2)
            {
                kakuritu_tum[i] = 1;
            }
            else
            {
                kakuritu_tum[i] = 0;
            }
        }
        Rondam_num_tum = (int)(Random.Range(0.1f, 2.0f) - 0.1f);
        if(Rondam_num_tum == 1)
        {
            Debug.Log("点火");
        }
        if(Rondam_num_tum == 0)
        {
            Debug.Log("点火失敗");
        }
        PlayerInfo.Instance.Health -= 5;
        PlayerInfo.Instance.Hunger -= 5;
        PlayerInfo.Instance.Thirst -= 5;

    }

    //ボタン3の処理
    public void botton3()
    {
        kakuritu_tum = new int[10];
        for (int i = 0; i < 10; i++)
        {
            if(i < kakuritu3)
            {
                kakuritu_tum[i] = 1;
            }
            else
            {
                kakuritu_tum[i] = 0;
            }
        }
        Rondam_num_tum = (int)(Random.Range(0.1f, 2.0f) - 0.1f);
        if(Rondam_num_tum == 1)
        {
            Debug.Log("点火");
        }
        if(Rondam_num_tum == 0)
        {
            Debug.Log("点火失敗");
        }
        Debug.Log("アイテム減少処理");
    }
}
