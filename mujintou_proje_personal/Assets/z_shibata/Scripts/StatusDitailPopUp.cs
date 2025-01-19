using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusDitailPopUp : MonoBehaviour
{
    [SerializeField] Text text_;
    public void HealthView()
    {
       text_.GetComponent<Text>().text = PlayerInfo.Instance.Health.ToString();
    }
    public void ThirstView()
    {
        text_.GetComponent<Text>().text = PlayerInfo.Instance.Thirst.ToString();
    }
    public void HungryView()
    {
        text_.GetComponent<Text>().text = PlayerInfo.Instance.Hunger.ToString();
    }
}
