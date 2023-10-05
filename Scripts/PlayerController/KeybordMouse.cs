using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMouse : PlayerController
{
    [SerializeField] private GameObject aimCircle;
    [SerializeField] private Hunger hunger;
    protected override void Start()
    {
        base.Start();
        hunger.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        engine.LookAtVec(aimCircle.transform.position);
        targetPos = (aimCircle.transform.position - transform.position).normalized;
        hunger.inUse.trigger = Convert.ToBoolean(Attack01);
    }

    protected override void InputToVelocity()
    {
        base.InputToVelocity();
        engine.velocityPlan += Move;
    }

    protected override Vector3 Move
    {
        get { return keyMap.Keybord.Move.ReadValue<Vector2>().normalized * speed.entity; }
    }
    public float Attack01
    {
        get { return keyMap.Keybord.Attack01.ReadValue<float>(); }
    }
}
