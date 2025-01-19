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
        //  ÉåÉVÉsï\Ç©ÇÁéÊìæ
        if (CraftSlotL != null && CraftSlotR != null)
        {
            switch (CraftSlotL.name)
            {
                //  ç∂Ç™êŒ
                case "Stone":
                    switch (CraftSlotR.name)
                    {
                        case "WoodEda":
                            //  êŒÇÃïÄ
                            break;
                        default:
                            //  âΩÇ‡çÏê¨Ç≈Ç´Ç»Ç¢
                            break;
                    }
                    break;
                case "WoodEda":
                    switch (CraftSlotR.name)
                    {
                        case "Stone":
                            //  êŒÇÃïÄ
                            break;
                        default:
                            //  âΩÇ‡Ç≈Ç´Ç»Ç¢
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
