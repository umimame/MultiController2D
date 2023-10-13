using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Pad : AimCircle
{
    protected override void Update()
    {
        inputAimPos = Input.mousePosition;
        aimPosInCamera = Camera.main.ScreenToWorldPoint(inputAimPos);
        base.Update();
    }
}
