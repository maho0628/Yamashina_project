using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onoe
{
    public class Button : MonoBehaviour
    {
        public static bool gameEndA = false;

        private void Start()
        {
            gameEndA = false;
        }
        public void OnClick()
        {
            gameEndA = true;
            Debug.Log("gameEndA:true");
        }
    }
}