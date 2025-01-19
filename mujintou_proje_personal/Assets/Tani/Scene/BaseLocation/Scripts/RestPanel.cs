using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestPanel : PanelBase
{
    [SerializeField]
    Button rest_button;
    [SerializeField]
    Text RestButtonText;
    [SerializeField]
    Text HealthChangeText;
    [SerializeField]
    Text HungerChangeText;
    [SerializeField]
    Text ThirstChangeText;
    [SerializeField]
    int health_change = 30;
    [SerializeField]
    int hunger_change = -30;
    [SerializeField]
    int thirst_change = -30;
    [SerializeField]
    Image poisonImage;
    [SerializeField]
    Image StarvingImage;
    [SerializeField]
    Image ThirstyImage;
    [SerializeField]
    GameObject ExtraEffectText;
    [SerializeField]
    GameObject fireBoost;//éƒìcí«â¡ïœêî
    PlayerInfo info;
    protected override void Start()
    {
        info = PlayerInfo.Instance;
        SetSortOrder(OrderOfUI.NormalPanel);
        rest_button.onClick.AddListener(Rest);
    }

    int prev_health ;
    int prev_hunger ;
    int prev_thirst;
    int poisonEffect = 0;
    int hungryEffect = 0;
    int thirstyEffect = 0;

    protected override void Update()
    {

         prev_health  = info.Health;
         prev_hunger = info.Hunger;
         prev_thirst = info.Thirst;
        if (info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned))
        {
            
            poisonEffect = -25;
            poisonImage.gameObject.SetActive(true);
        }
        else
        {
            poisonEffect = 0;
            poisonImage.gameObject.SetActive(false);
        }

        if(info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Hungry))
        {
            float mul = ((20 - PlayerInfo.Instance.Hunger) / 20.0f) + 1;
            hungryEffect = (int)(-20 * mul);
            StarvingImage.gameObject.SetActive(true);
        }
        else
        {
            hungryEffect = 0;
            StarvingImage.gameObject.SetActive(false);
        }

        if(info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Thirsty))
        {
            float mul = ((20 - PlayerInfo.Instance.Thirst) / 20.0f) + 1;
            thirstyEffect = (int)(-20 * mul);
            ThirstyImage.gameObject.SetActive(true);
        }
        else
        {
            thirstyEffect = 0;
            ThirstyImage.gameObject.SetActive(false);
        }


        HealthChangeText.text = $"<b>{prev_health}%  >> " +
            $" {Mathf.Clamp(prev_health + (info.Fire > 0 ? (int)(health_change * 1.5f) + poisonEffect + hungryEffect : health_change + poisonEffect + hungryEffect), 0, 100)}%</b>";

        HungerChangeText.text = $"<b>{prev_hunger}%  >> " +
          $" {Mathf.Clamp(prev_hunger + hunger_change + thirstyEffect, 0, 100)}%</b>";

        ThirstChangeText.text = $"<b>{prev_thirst}%  >> " +
          $" {Mathf.Clamp(prev_thirst + thirst_change, 0, 100)}%</b>";
        if (!poisonImage.gameObject.activeSelf)
        {
            if (!StarvingImage.gameObject.activeSelf)
            {
                if (!ThirstyImage.gameObject.activeSelf)
                {
                    ExtraEffectText.SetActive(false);
                    return;
                }
            }
        }
        ExtraEffectText.SetActive(true);
    }
    private void OnEnable()
    {
        RestButtonText.text = PlayerInfo.Instance.Day.isDayTime ? "ñÈÇ‹Ç≈ãxÇﬁ" : "êáñ∞ÇÇ∆ÇÈ";
        //éƒìcí«â¡Ç±Ç±Ç©ÇÁ
        if(PlayerInfo.Instance.Fire >= 1)
        { 
            fireBoost.SetActive(true); 
        }
        else if(PlayerInfo.Instance.Fire < 1)
        {
            fireBoost.SetActive(false);
        }
        //Ç±Ç±Ç‹Ç≈
    }

    void Rest()
    {
        Fading fading = (Fading)GameObject.FindAnyObjectByType(typeof(Fading));
        if (!fading) return;
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() =>
        {
            var info = PlayerInfo.Instance;
            info.Health = Mathf.Clamp(prev_health + (info.Fire > 0 ? (int)(health_change * 1.5f) + poisonEffect + hungryEffect : health_change + poisonEffect + hungryEffect), 0, 100);
            info.Hunger = Mathf.Clamp(prev_hunger + hunger_change + thirstyEffect, 0, 100);
            info.Thirst = Mathf.Clamp(prev_thirst + thirst_change, 0, 100);
            info.ActionValue += Mathf.CeilToInt(info.MaxActionValue / 2.0f);
            info.DoAction();
     
            BaseLocationDaytimeController controller = (BaseLocationDaytimeController)GameObject.FindAnyObjectByType(typeof(BaseLocationDaytimeController));
            controller.ChangeBaseLocation();
        });

        
        
    }

}
