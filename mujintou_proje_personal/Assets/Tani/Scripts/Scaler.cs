using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Scaler : MonoBehaviour
{
    [Header("ƒXƒP[ƒ‹‚³‚ê‚é‚Ì‚ÍXY‚Ì‚Ý")]
    [SerializeField,Min(1f)] float scale = 1.5f;
    [SerializeField] float time = 0.5f;


    private void Update()
    {
        
    }

    public void MakeLarger()
    {
        StartCoroutine(MakeLargerCoroutine());
    }

    public void MakeSmaller()
    {
        StartCoroutine(MakeSmallerCoroutine());
    }

    IEnumerator MakeLargerCoroutine()
    {
        transform.localScale = Vector3.one;
        while (true)
        {
            Vector3 current = gameObject.transform.localScale;
            transform.localScale = current + new Vector3((scale / time) * Time.deltaTime, (scale / time) * Time.deltaTime, 1);
            if(transform.localScale.x >= scale)
            {
                transform.localScale = Vector3.one * scale;
                break;
            }
            yield return null;
        }
    }

    IEnumerator MakeSmallerCoroutine()
    {
        transform.localScale = Vector3.one * scale;
        while (true)
        {
            Vector3 current = gameObject.transform.localScale;
            transform.localScale = current - new Vector3((scale / time) * Time.deltaTime, (scale / time) * Time.deltaTime, 1);
            if (transform.localScale.x <= 1.0f)
            {
                transform.localScale = Vector3.one;
                break;
            }
            yield return null;
        }
    }

}
