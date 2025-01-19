using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AreaNameText : MonoBehaviour
{

    [SerializeField]
    GameObject DayInfoPanel_prefab;

    float inTime = 0.75f;
    float outTime = 0.75f;
    float stayTime = 1f;

    public IEnumerator  ShowAreaText()
    {
        var g=  Instantiate<GameObject>(DayInfoPanel_prefab);
        var c = g.GetComponent<Canvas>();
        g.transform.position = new Vector3(c.pixelRect.width, c.pixelRect.height);
        g.GetComponentInChildren<Text>().text = $"{PlayerInfo.Instance.Day.day}“ú–Ú";
        yield return new WaitForSeconds(2f);
        var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
        fade.Fade(Fading.type.FadeIn);
        Destroy(g);

    }

}
