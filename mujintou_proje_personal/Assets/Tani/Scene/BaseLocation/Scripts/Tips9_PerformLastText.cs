using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips9_PerformLastText : MonoBehaviour
{
    [SerializeField]
    Button button;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            var c = (BL_Tu_SceneController)FindAnyObjectByType(typeof(BL_Tu_SceneController));
            c.LastText();
        });
    }
}
