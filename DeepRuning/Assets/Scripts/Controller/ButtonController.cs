using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    bool pointingPrev;
    bool pointing;
    public Action onclick;
    public Action onPress;
    public Action onRelease;
    void Update()
    {
        if (!pointingPrev && pointing)
        {
            if (onclick != null)
            {
                onclick();
            }
        }
        else if (pointingPrev && pointing)
        {
            if (onPress != null)
            {
                onPress();
            }
        }
        else if (pointingPrev && !pointing)
        {
            if (onRelease != null)
            {
                onRelease();
            }
        }

        if (pointingPrev != pointing)
        {
            pointingPrev = pointing;
        }
    }

    public void OnButtonPointDown()
    {
        pointing = true;
    }
    public void OnButtonPointUp()
    {
        pointing = false;
    }
}
