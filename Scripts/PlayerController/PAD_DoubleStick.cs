using My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PAD_DoubleStick : PlayerController
{
    [SerializeField] private Vector3 aimMovePlan;
    [SerializeField] private Vector2 beforeAimMove;
    [SerializeField] private CircleClamp moveCircle;
    protected override void Start()
    {
        base.Start();
        moveCircle.Initialize();
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
                moveCircle.moveObject.transform.position = transform.position;
                moveCircle.moveObject.transform.position += new Vector3(inputVelocityPlan.x, inputVelocityPlan.y) * moveCircle.radius;

                clamp.moveObject.transform.position = transform.position;
                clamp.moveObject.transform.position += aimMovePlan * clamp.radius;
                LookAtMovingDirection();
                break;
            case State.Death:
                engine.CollDisabled();
                Death();
                break;
        }

        if (state == State.Idol)
        {
            alive = true;
        }
    }

    protected override void LastUpdate()
    {
        base.LastUpdate();
        moveCircle.Limit();
    }

    protected override void InputToVelocityPlan()
    {
        base.InputToVelocityPlan();
        engine.velocityPlan += inputVelocityPlan * speed.entity;
    }

    /// <summary>
    /// ìÆÇ¢ÇƒÇ¢ÇÈï˚å¸Çå¸Ç≠
    /// </summary>
    public void LookAtMovingDirection()
    {

        // à⁄ìÆÇÃì¸óÕÇ™Ç†ÇÈèÍçáÇÃÇ›ï˚å¸ÇïœçXÇ∑ÇÈ
        //if (beforeVec != new Vector2(0, 0) && inputVelocityPlan != new Vector3(0, 0))
        //{
        //    engine.LookAtVec(clamp.moveObject.transform.position);
        //}
        //beforeVec = inputVelocityPlan;

        if(aimMovePlan != new Vector3(0, 0))
        {
            engine.LookAtVec(clamp.moveObject.transform.position);
        }
        else if (beforeAimMove == new Vector2(0, 0) && aimMovePlan == new Vector3(0, 0))
        {
            if (beforeVec != new Vector2(0, 0) && inputVelocityPlan != new Vector3(0, 0))
            {
                engine.LookAtVec(moveCircle.moveObject.transform.position);
            }
        }
        beforeVec = inputVelocityPlan;
        beforeAimMove = aimMovePlan;
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


    public void OnAimMove(InputValue value)
    {
        if (alive)
        {
            aimMovePlan = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0.0f);
        }
    }
}
