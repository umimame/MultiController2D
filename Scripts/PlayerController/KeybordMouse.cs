using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using UnityEngine.InputSystem;

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
    }

    protected override void InputToVelocityPlan()
    {
        base.InputToVelocityPlan();

        engine.velocityPlan += inputVelocityPlan * speed.entity;
    }

    public void OnMove(InputValue value)
    {
        if (alive)
        {
            inputVelocityPlan = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0.0f);
        }
    }
    public void OnAttack1(InputValue value)
    {
        if (alive)
        {
            hunger.inUse[0].trigger = Convert.ToBoolean(value.Get<float>());
        }
    }
    public void OnAttack2(InputValue value)
    {
        if (alive)
        {
            hunger.inUse[1].trigger = Convert.ToBoolean(value.Get<float>());
        }
    }
}
