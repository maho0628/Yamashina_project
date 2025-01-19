using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class Text_Method : MonoBehaviour
{
    [SerializeField] Text textObject;
    [System.NonSerialized] public string now_Text;

    void Start()
    {
    }

    void Update()
    {
    }

    //テキスト表示
    public void Text_Disply(string text)
    {
        string sentence = text;
        textObject.text = sentence;       
    }
}
