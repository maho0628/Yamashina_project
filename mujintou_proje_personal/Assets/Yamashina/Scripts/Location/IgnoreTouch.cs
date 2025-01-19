using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnoreTouch : Button,ICanvasRaycastFilter
{
    // Start is called before the first frame update
 public bool IsRaycastLocationValid(Vector2 sp,Camera eventCamera)
    {
        return false;
    }
}
