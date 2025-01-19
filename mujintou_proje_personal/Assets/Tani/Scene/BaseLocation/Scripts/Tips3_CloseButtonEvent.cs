using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips3_CloseButtonEvent : MonoBehaviour
{
    [SerializeField]
    Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            var c = (BL_Tu_2_SceneController)FindAnyObjectByType(typeof(BL_Tu_2_SceneController));
            c.NextText();
        });
    }

}
