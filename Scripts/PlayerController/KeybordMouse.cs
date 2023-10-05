using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMouse : PlayerController
{
    [SerializeField] private GameObject aimCircle;
    [SerializeField] private Hunger hunger;
    public bool left;
    protected override void Start()
    {
        base.Start();
        hunger.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        engine.LookAtVec(aimCircle.transform.position);
        if(Attack01 == true)
        {
            hunger.inUse.exe = true;
        }
        left = Attack01;
    }

    protected override void InputToVelocity()
    {
        base.InputToVelocity();
        engine.velocityPlan += Move;
    }

    protected override Vector2 Move
    {
        get { return keyMap.Keybord.Move.ReadValue<Vector2>().normalized * speed.entity; }
    }
    public bool Attack01
    {
        get { return keyMap.Keybord.Attack01.ReadValue<bool>(); }
    }
}
