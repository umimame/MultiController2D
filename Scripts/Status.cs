using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using My;

/// <summary>
/// パラメータ<br/>
/// entity:実際の数値<br/>
/// max:数値の最大値
/// </summary>
[Serializable] public class Parameter
{
    [field: SerializeField] // PropertyをInspectorに表示
    public float entity { get; set; }
    [field: SerializeField] public float max { get; set; }
    [field: SerializeField] public float recover { get;set; }

    public Parameter() { }

    public void Initialize()
    {
        entity = max;
    }

    public void Update()
    {
        entity += recover;
        if (entity > max) { entity = max; }
    }
}


/// <summary>
/// Playerオブジェクトや敵オブジェクト、弾丸などに使用
/// </summary>
public class Chara : MonoBehaviour
{
    public enum State
    {
        Spawn,
        Idol,
        Death,
        Non,
    }

    [field: SerializeField] public State state { get; set; }
    [field: SerializeField] public Parameter hp { get; set; }
    [field: SerializeField] public Parameter speed { get; set; }
    [field: SerializeField] public Engine engine { get; set; }
    [field: SerializeField] public Vector2 targetPos { get; set; }
    protected virtual void Start()
    {
        hp.Initialize();
        speed.Initialize();
        state = State.Spawn;
    }

    /// <summary>
    /// velocityのリセット<br/>
    /// 継承先で追記
    /// </summary>
    protected virtual void HeadUpdate()
    {


    }
    /// <summary>
    /// HeadUpdateとLastUpdateの間<br/>
    /// 主にState処理に入る<br/>
    /// 継承先で追記
    /// </summary>
    protected virtual void MiddleUpdate()
    {

    }


    /// <summary>
    /// Updateの最後<br/>
    /// 継承先で追記
    /// </summary>
    protected virtual void LastUpdate()
    {
        engine.VelocityResult();
        engine.velocityPlan = new Vector2(0.0f, 0.0f);
    }
    protected virtual void Update()
    {
        HeadUpdate();
        MiddleUpdate();
        LastUpdate();

    }


    /// <summary>
    /// 死亡時の挙動<br/>
    /// 継承先で追記
    /// </summary>
    protected virtual void Death()
    {
        Destroy(gameObject);
    }

}