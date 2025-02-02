using System.Collections;
using System.Collections.Generic;
using Toshiki;
using UnityEngine;
using UnityEngine.UI;


public class Shageki_hakoChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer thisRender;

    [SerializeField] Sprite[] Sprites;


    private void Start()
    {

        int tmp = Random.Range(0, 3);

        if (tmp == 2)
        {
            GetComponent<Shageki_mato>().AddScore(Shageki_Manager.instance.point_Small);

            //StartCoroutine(SetAddScore());
        }

        thisRender.sprite = Sprites[tmp];


    }

    IEnumerator SetAddScore()
    {
        yield return new WaitForEndOfFrame();
      //  GetComponent<Shageki_mato>().AddScore(Shageki_Manager.instance.point_Small);

        yield return null;
    }
}
