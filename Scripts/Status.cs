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


public class Chara : MonoBehaviour
{
    [field: SerializeField] public Parameter hp { get; set; }
    [field: SerializeField] public Parameter speed { get; set; }
    [field: SerializeField] public Engine engine { get; set; }
    protected virtual void Start()
    {
        Debug.Log("Initialize");
        speed.Initialize();
        engine = GetComponent<Engine>();
    }

    protected virtual void Update()
    {
        engine.velocityPlan = new Vector2(0.0f, 0.0f);
        engine.velocityPlan += KeyMapCallBack.Move.normalized * speed.entity;

        engine.VelocityResult();

    }

}