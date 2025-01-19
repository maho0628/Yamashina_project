using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConditionPresenter : MonoBehaviour
{
    [SerializeField]
    GameObject poisonImage;
    [SerializeField]
    GameObject hungryImage;
    [SerializeField]
    GameObject thristyImage;

    private void Update()
    {
        if (PlayerInfo.InstanceNullable == null) return;
        poisonImage.SetActive(PlayerInfo.Instance.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned));
        hungryImage.SetActive(PlayerInfo.Instance.IsPlayerConditionEqualTo(PlayerInfo.Condition.Hungry));
        thristyImage.SetActive(PlayerInfo.Instance.IsPlayerConditionEqualTo(PlayerInfo.Condition.Thirsty));


    }
}
