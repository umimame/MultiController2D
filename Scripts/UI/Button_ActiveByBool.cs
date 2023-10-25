using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ActiveByBool : ButtonClass
{
    [field: SerializeField] public bool active {  get; set; }
    private Instancer coverPanel;
    public override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        if (active == false && coverPanel.Displaying == false)
        {

        }
    }
    public override void ButtonOnClick()
    {
        base.ButtonOnClick();
    }
}
