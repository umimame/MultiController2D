using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                clamp.moveObject.transform.position += new Vector3(Move.x, Move.y) * speed.entity;
                break;
            case State.Death:
                Death();
                break;
        }
    }

    protected override void LastUpdate()
    {
        base.LastUpdate();
        LookAtMovingDirection();
        hunger.inUse.trigger = Convert.ToBoolean(keyMap.Pad.Attack1.ReadValue<float>());
    }

    protected override void InputToVelocityPlan()
    {
        base.InputToVelocityPlan();
        engine.velocityPlan += Move * speed.entity;
    }

    /// <summary>
    /// 動いている方向を向く
    /// </summary>
    public void LookAtMovingDirection()
    {

        // 移動の入力がある場合のみ方向を変更する
        if (beforeVec != new Vector2(0, 0) && Move != new Vector3(0, 0))
        {
            engine.LookAtVec(clamp.moveObject.transform.position);
        }
        beforeVec = Move;
    }

    /// <summary>
    /// keyMap.Padの省略プロパティ
    /// </summary>
    protected override Vector3 Move
    {
        get {
            return this.keyMap.Pad.Move.ReadValue<Vector2>(); }
    }

    protected override float Attack1
    {
        get
        {
            Debug.Log(gameObject.tag); 
            return this.keyMap.Pad.Attack1.ReadValue<float>(); }
    }

}
