using UnityEngine;


/// <summary>
/// Animatorのステート名をドロップダウンで選べるようにする属性
/// </summary>
public class AnimatorStateDropdownAttribute : PropertyAttribute
{
    public string ControllerFieldName;

    public AnimatorStateDropdownAttribute(string controllerFieldName)
    {
        ControllerFieldName = controllerFieldName;
    }
}

