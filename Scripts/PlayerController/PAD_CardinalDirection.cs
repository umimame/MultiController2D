using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PAD_CardinalDirection : PlayerController
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
                clamp.moveObject.transform.position += new Vector3(inputVelocityPlan.x, inputVelocityPlan.y) * clamp.radius;

                LookAtMovingDirection();
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
    /// “®‚¢‚Ä‚¢‚é•ûŒü‚ğŒü‚­
    /// </summary>
    public void LookAtMovingDirection()
    {

        // ˆÚ“®‚Ì“ü—Í‚ª‚ ‚éê‡‚Ì‚İ•ûŒü‚ğ•ÏX‚·‚é
        if (beforeVec != new Vector2(0, 0) && inputVelocityPlan != new Vector3(0, 0))
        {
            engine.LookAtVec(clamp.moveObject.transform.position);
        }
        beforeVec = inputVelocityPlan;
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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (alive)
        {
            inputVelocityPlan = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0);

        }
    }
}
