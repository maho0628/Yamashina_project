using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusImage : MonoBehaviour
{
    Image image;
    [SerializeField] Status_Type status_type = Status_Type.Health;
    PlayerInfo info;


    public enum Status_Type
    {
        Health,Hunger,Thirst
    }
    // Start is called before the first frame update
    void Start()
    {
        info = PlayerInfo.Instance;
        image = GetComponent<Image>();
        image.type = Image.Type.Filled;
        
        switch (status_type)
        {
            case Status_Type.Health:
                image.fillMethod = Image.FillMethod.Radial360;
                image.fillOrigin = 2;
                image.fillClockwise = false;
                
                break;
            case Status_Type.Hunger:
                image.fillMethod = Image.FillMethod.Vertical;
                
                break;

            case Status_Type.Thirst:
                image.fillMethod = Image.FillMethod.Vertical;
                break;
        }

    }


    void Update()
    {
        if (PlayerInfo.InstanceNullable == null) return;
        
        image.fillAmount = info.GetStatusPercent((int)status_type);
        switch (status_type)
        {
            case Status_Type.Health:
                if (image.fillAmount <= 0.45f)
                {
                    image.color = new Color(255 / 255.0f, 56 / 255.0f, 52 / 255.0f);

                }
                else
                {
                    image.color = new Color(143 / 255.0f, 195 / 255.0f, 31 / 255.0f);
                    if (PlayerInfo.Instance.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned))
                    {
                        image.color = new Color(166 / 255.0f, 28 / 255.0f, 195 / 255.0f);
                    }
                }
                break;
            case Status_Type.Hunger:
                if (image.fillAmount <= 0.45f)
                {
                    image.color = new Color(234 / 255.0f, 85 / 255.0f, 20 / 255.0f);

                }
                else
                {
                    image.color = new Color(243 / 255.0f, 152 / 255.0f, 0 / 255.0f);
                }

                break;
            case Status_Type.Thirst:
                
                if (image.fillAmount <= 0.45f)
                {
                    image.color = new Color(234 / 255.0f, 85 / 255.0f, 20 / 255.0f);
                
                }
                else
                {
                    image.color = new Color(46 / 255.0f, 167 / 255.0f, 224 / 255.0f);
                }
                break;
        }
        
    }
}
