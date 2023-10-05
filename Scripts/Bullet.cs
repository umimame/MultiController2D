using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Bullet : Chara
{
    [SerializeField] private AnimationCurve moveX;
    [SerializeField] private AnimationCurve moveY;
    [SerializeField] private float time;
    [field: SerializeField] public float lifeTime { get; set; }
    protected override void Start()
    {
        base.Start();
        time = 0.0f;
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        engine.velocityPlan += transform.rotation * new Vector3(moveX.Evaluate(time), moveY.Evaluate(time)) * speed.entity;
        engine.VelocityResult();

    }

    protected override void Death()
    {
        if(time >= lifeTime)
        {
            if(lifeTime <= 0)
            {
                Debug.Log("’e‚Ì¶‘¶ŽžŠÔ‚ª0‚Å‚·B");
            }
            base.Death();
        }
    }
}
