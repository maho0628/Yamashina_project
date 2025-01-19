using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerSystem : MonoBehaviour
{
    public enum Items
    {
        Stone,
        Wood,
        WoodEda,
    }
    public GameObject[] craftPrefab;
    public GameObject CraftSlotL, CraftSlotR;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  ���V�s�\����擾
        if (CraftSlotL != null && CraftSlotR != null)
        {
            switch (CraftSlotL.name)
            {
                //  ������
                case "Stone":
                    switch (CraftSlotR.name)
                    {
                        case "WoodEda":
                            //  �΂̕�
                            break;
                        default:
                            //  �����쐬�ł��Ȃ�
                            break;
                    }
                    break;
                case "WoodEda":
                    switch (CraftSlotR.name)
                    {
                        case "Stone":
                            //  �΂̕�
                            break;
                        default:
                            //  �����ł��Ȃ�
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
