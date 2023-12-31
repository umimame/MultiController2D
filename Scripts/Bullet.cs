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
    [SerializeField] private AudioClip sound;
    protected override void Start()
    {
        base.Start();
        time = 0.0f;
        pow.Initialize();
        World.instance.audioSource.PlayOneShot(sound);
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        engine.velocityPlan += engine.transform.rotation * new Vector3(moveX.Evaluate(time), moveY.Evaluate(time)) * speed.entity;
        engine.VelocityResult();

        LifeTimer(Death);
    }

    protected override void Death()
    {
            base.Death();
    }

    /// <summary>
    /// 生存時間を超えると与えられた処理(基本は破棄処理)<br/>
    /// 引数は「返り値がvoidの関数」を指定する
    /// </summary>
    /// <param name="action"></param>
    public void LifeTimer(Action action)
    {

        if (time >= lifeTime)
        {
            if (lifeTime <= 0)
            {
                Debug.Log("弾の生存時間が0です。");
            }
            action();
        }
    }

    protected virtual void Hit(Collider2D collision)
    {
        if (collision.transform.tag != engine.transform.tag)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Hit(collision);
    }
}
