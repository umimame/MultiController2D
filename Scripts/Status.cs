using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // Inspectorに表示
public class Status
{

}

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

    public Parameter() { }

    public void Initialize()
    {
        entity = max;
    }
}


/// <summary>
/// Playerオブジェクトや敵オブジェクト、弾丸などに使用
/// </summary>
public class Chara : MonoBehaviour
{
    [field: SerializeField] public Parameter hp { get; set; }
    [field: SerializeField] public Parameter speed { get; set; }
    [field: SerializeField] public Engine engine { get; set; }
    [field: SerializeField] public Vector2 targetPos { get; set; }
    protected virtual void Start()
    {
        hp.Initialize();
        speed.Initialize();
    }

    /// <summary>
    /// velocityのリセット<br/>
    /// 継承先で追記
    /// </summary>
    protected virtual void HeadUpdate()
    {
        engine.velocityPlan = new Vector2(0.0f, 0.0f);


    }
    /// <summary>
    /// HeadUpdateとLastUpdateの間<br\>
    /// 継承先で主に追記
    /// </summary>
    protected virtual void MiddleUpdate()
    {

    }

    protected virtual void LastUpdate()
    {
        engine.VelocityResult();
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