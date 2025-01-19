using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonMotion : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] Vector3 startP;
    [SerializeField] Vector3 endP;
    [SerializeField] float time;
    float t;

    [SerializeField] Color startC;
    [SerializeField] Color endC;
    public void helpEnter()
    {
        text.gameObject.SetActive(true);
    }
    public void helpExit()
    {
        text.GetComponent<RectTransform>().localPosition = startP;
        t = 0;
        text.gameObject.SetActive(false);
    }
    private void Update()
    {
        t += time * Time.deltaTime;
        text.GetComponent<RectTransform>().localPosition = Vector3.Lerp(startP, endP, t);
        text.GetComponent<Text>().color = Color.Lerp(startC,endC,t);

    }
}
