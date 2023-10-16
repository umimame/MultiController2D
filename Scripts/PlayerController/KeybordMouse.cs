using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMouse : PlayerController
{

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void HeadUpdate()
    {
        base.HeadUpdate();
    }

    protected override void MiddleUpdate()
    {
        if (hp.entity <= 0) { state = State.Death; }

        switch (state)
        {
            case State.Spawn:
                state = State.Idol;
                break;
            case State.Idol:
                base.MiddleUpdate();
                clamp.moveObject.transform.position = AddFunction.CameraToMouse();
                break;
            case State.Death:
                Death();
                break;
        }
    }

    protected override void LastUpdate()
    {
        base.LastUpdate();
        engine.LookAtVec(clamp.moveObject.transform.position);
        targetPos = (clamp.moveObject.transform.position - transform.position).normalized;
        hunger.inUse.trigger = Convert.ToBoolean(Attack1);
    }

    protected override void InputToVelocityPlan()
    {
        base.InputToVelocityPlan();
        engine.velocityPlan += Move;
    }

    protected override Vector3 Move
    {
        get { return keyMap.Keybord.Move.ReadValue<Vector2>().normalized * speed.entity; }
    }
    protected override float Attack1
    {
        get { return keyMap.Keybord.Attack1.ReadValue<float>(); }
    }

}
