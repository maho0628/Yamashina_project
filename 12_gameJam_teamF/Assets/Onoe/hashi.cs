using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onoe
{
    public class hashi : MonoBehaviour
    {
        Vector2 mousePos = Vector2.zero;
        Vector3 mPos, worldPos;
        bool Drag = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mPos = Input.mousePosition;
                Drag = true;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Drag = false;
            }

            if(Drag)
            {
                worldPos=Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, mPos.z));
                transform.position = worldPos;
            }
        }
    }
}
