using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : PlayerController
{
    [SerializeField] CircleClamp circleClamp;
    protected override void Start()
    {
        base.Start();
        circleClamp.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        circleClamp.moveObject.transform.position = transform.position;
        circleClamp.moveObject.transform.position += new Vector3(Move.x, Move.y) * speed.entity;
        circleClamp.Limit();
        engine.LookAtObject(circleClamp.moveObject.transform.position);

        if(beforeVec != new Vector2(0, 0) && Move != new Vector2(0, 0))
        {
            engine.LookAtObject(Move);
        }
            beforeVec = Move;
        
    }

    protected override void InputToVelocity()
    {
        base.InputToVelocity();
        engine.velocityPlan += Move * speed.entity;
    }

    public Vector2 Move
    {
        get { return keyMap.Pad.Move.ReadValue<Vector2>(); }
    }
}
