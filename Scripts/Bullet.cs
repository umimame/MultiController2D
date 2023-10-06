using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Bullet : Chara
{
    [SerializeField] private AnimationCurve moveX;
    [SerializeField] private AnimationCurve moveY;
    [SerializeField] private float time;
    [field: SerializeField] public Parameter pow { get; set; }
    [field: SerializeField] public float lifeTime { get; set; }
    protected override void Start()
    {
        base.Start();
        time = 0.0f;
        pow.Initialize();
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        engine.velocityPlan += transform.rotation * new Vector3(moveX.Evaluate(time), moveY.Evaluate(time)) * speed.entity;
        engine.VelocityResult();

        LifeTimer(Death);
    }

    protected override void Death()
    {
            base.Death();
    }

    /// <summary>
    /// �������Ԃ𒴂���Ɣj������<br/>
    /// �����́u�Ԃ�l��void�̊֐��v���w�肷��
    /// </summary>
    /// <param name="action"></param>
    public void LifeTimer(Action action)
    {

        if (time >= lifeTime)
        {
            if (lifeTime <= 0)
            {
                Debug.Log("�e�̐������Ԃ�0�ł��B");
            }
            action();
        }
    }

    private void Hit(Collision2D collision)
    {
        if(collision.transform.tag != transform.tag)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != engine.transform.tag)
        {
            Death();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != engine.transform.tag)
        {
            Death();
        }
    }

}
