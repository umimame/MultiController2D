using My;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : SpriteOrImage
{
    [field: SerializeField] public bool active { get; private set; }
    [SerializeField] private Interval interval;
    [SerializeField] private bool hide;

    public void Enable(float activeTime)
    {
        active = true;
        interval.Initialize(true, activeTime);
        hide = true;
    }

    public void Update()
    {

        if (active)
        {
            interval.Update();
            interval.Launch(Flash);
        }
    }

    public void Flash()
    {
        if(hide == true)
        {
            Alpha = 0.0f;
            hide = false;
        }
        else
        {
            Alpha = 1.0f;
            hide = true;
        }
    }
}
