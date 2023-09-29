using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMouse : PlayerController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
    }

    protected override void InputToVelocity()
    {
        base.InputToVelocity();
        engine.velocityPlan += keyMap.Keybord.Move.ReadValue<Vector2>().normalized * speed.entity;
    }
}
