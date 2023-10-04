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

        LookAtMovingDirection();
    }

    protected override void InputToVelocity()
    {
        base.InputToVelocity();
        engine.velocityPlan += Move * speed.entity;
    }

    /// <summary>
    /// �����Ă������������
    /// </summary>
    public void LookAtMovingDirection()
    {

        // �ړ��̓��͂�����ꍇ�̂ݕ�����ύX����
        if (beforeVec != new Vector2(0, 0) && Move != new Vector2(0, 0))
        {
            engine.LookAtVec(circleClamp.moveObject.transform.position);
        }
        beforeVec = Move;
    }

    /// <summary>
    /// keyMap.Pad�̏ȗ��v���p�e�B
    /// </summary>
    protected override Vector2 Move
    {
        get { return keyMap.Pad.Move.ReadValue<Vector2>(); }
    }

}
