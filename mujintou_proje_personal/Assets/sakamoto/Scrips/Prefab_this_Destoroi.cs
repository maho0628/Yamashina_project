using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Prefab_this_Destoroi : MonoBehaviour
{
    Vector3 pos;
    Color color;
    bool a_Change;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Update", 1.0f);
        StartCoroutine(Prehab_Down());
        a_Change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (a_Change)
        {
            if (this.transform.localPosition.y < 0)
            {
                color = GetComponent<Image>().color;
                color.a = 1.0f - (this.transform.localPosition.y / -300);
                GetComponent<Image>().color = color;
            }
        }
        if (this.transform.localPosition.y < -300)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Prehab_Down() 
    {
        yield return new WaitForSeconds(1);
        a_Change |= true;
        while (true)
        {
            gameObject.transform.Translate(new Vector3(0.0f, -8 * Time.deltaTime, 0.0f));
            yield return null;
        }
    }    
}
