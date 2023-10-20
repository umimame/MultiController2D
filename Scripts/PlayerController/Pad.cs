using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pad : PlayerController
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
                clamp.moveObject.transform.position = transform.position;
                clamp.moveObject.transform.position += new Vector3(inputVelocityPlan.x, inputVelocityPlan.y) * speed.entity;

                LookAtMovingDirection();
                //hunger.inUse[0].trigger = Convert.ToBoolean(Attack1);
                //hunger.inUse[1].trigger = Convert.ToBoolean(Attack2);
                break;
            case State.Death:
                engine.CollDisabled();
                Death();
                break;
        }

        if(state == State.Idol)
        {
            alive = true;
        }
    }

    protected override void LastUpdate()
    {
        base.LastUpdate();
    }

    protected override void InputToVelocityPlan()
    {
        base.InputToVelocityPlan();
        engine.velocityPlan += inputVelocityPlan * speed.entity;
    }

    /// <summary>
    /// 動いている方向を向く
    /// </summary>
    public void LookAtMovingDirection()
    {

        // 移動の入力がある場合のみ方向を変更する
        if (beforeVec != new Vector2(0, 0) && inputVelocityPlan != new Vector3(0, 0))
        {
            engine.LookAtVec(clamp.moveObject.transform.position);
        }
        beforeVec = inputVelocityPlan;
    }

    /// <summary>
    /// keyMap.Padの省略プロパティ
    /// </summary>
    protected override Vector3 Move
    {
        get { return keyMap.Pad.Move.ReadValue<Vector2>(); }
    }

    protected override float Attack1
    {
        get { return keyMap.Pad.Attack1.ReadValue<float>(); }
    }

    public float Attack2
    {
        get { return keyMap.Pad.Attack2.ReadValue<float>(); }
    }
    public float Attack3
    {
        get { return keyMap.Pad.Attack3.ReadValue<float>(); }
    }
    public float Attack4
    {
        get { return keyMap.Pad.Attack4.ReadValue<float>(); }
    }

    public void OnMove(InputValue value)
    {
        inputVelocityPlan = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0.0f);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVelocityPlan = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0);
    }
}
