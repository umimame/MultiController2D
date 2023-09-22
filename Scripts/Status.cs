using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // Inspector�ɕ\��
public class Status
{

}

/// <summary>
/// �p�����[�^<br/>
/// entity:���ۂ̐��l<br/>
/// max:���l�̍ő�l
/// </summary>
[Serializable] public class Parameter
{
    [field: SerializeField] // Property��Inspector�ɕ\��
    public float entity { get; set; }
    [field: SerializeField] public float max { get; set; }

    public Parameter()
    {
        entity = max;
    }
}

[Serializable] public class Engine
{
    [field: SerializeField] public Rigidbody2D rb { get; set; }
    [field: SerializeField] public BoxCollider2D collider { get; set; }
    [field: SerializeField] public SpriteRenderer sprite { get; set; }
    [field: SerializeField] public float time { get; set; }

}

public class Chara : MonoBehaviour
{
    [field: SerializeField] public Parameter hp { get; set; }
    [field: SerializeField] public Parameter speed { get; set; }
    [field: SerializeField] public Engine engine { get; set; }
    protected virtual void Start()
    {
        Debug.Log("Initialize");
        hp = new Parameter();
        speed = new Parameter();

        EngineInitialize();
    }

    protected virtual void Update()
    {
        engine.rb.velocity = KeyMapCallBack.Move.normalized * speed.entity;
        Debug.Log(engine.rb.velocity);
    }

    private void EngineInitialize()
    {
        engine = new Engine();
        engine.rb = GetComponent<Rigidbody2D>();
        engine.collider = GetComponent<BoxCollider2D>();
        engine.sprite = GetComponent<SpriteRenderer>();
        engine.time = 0.0f;
    }
}