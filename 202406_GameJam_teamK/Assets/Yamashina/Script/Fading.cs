using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Fading : MonoBehaviour
{
    [SerializeField]
    Image fade_image;
    [SerializeField,Min(0)]
    public float fading_time = 1.0f;


    public UnityEvent OnFadeEnd { get; set; } = new UnityEvent();
    public enum type { FadeIn,FadeOut};
    public void Fade(type type)
    {

        if (fading_time == 0) return;
        if (!fade_image) return;
        fade_image.gameObject.SetActive(true);
        StartCoroutine(type.ToString());
        
    }

    IEnumerator FadeIn()
    {
        int power = 3;
        float f = power / fading_time;
        float time = 0;

        while (true)
        {
            //time += Time.deltaTime;
            //float a = Mathf.Pow(10, f * time) / 1000;

            time += Time.deltaTime;
            float a = time / fading_time;
            fade_image.color = new Color(fade_image.color.r, fade_image.color.g, fade_image.color.b, 1- a);
            //Debug.Log(fade_image.color);
            if(a >= 1)
            {
                break;
            }
            yield return null;
        }

        
        fade_image.gameObject.SetActive(false);
        OnFadeEnd.Invoke();
        OnFadeEnd.RemoveAllListeners();
    }

    IEnumerator FadeOut()
    {


        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            float a = time / fading_time;
            fade_image.color = new Color(fade_image.color.r, fade_image.color.g, fade_image.color.b, a);
            if (a >= 1)
            {
                break;
            }
            //Debug.Log(fade_image.color);
            yield return null;
        }


        //fade_image.gameObject.SetActive(false);
        OnFadeEnd.Invoke();
        OnFadeEnd.RemoveAllListeners();
    }

    public void SetFadeImageActivaton(bool isActive)
    {
        fade_image.gameObject.SetActive(isActive);
    }

}
