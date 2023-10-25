using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using My;

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
/// Player�I�u�W�F�N�g��G�I�u�W�F�N�g�A�e�ۂȂǂɎg�p
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
    /// velocity�̃��Z�b�g<br/>
    /// �p����ŒǋL
    /// </summary>
    protected virtual void HeadUpdate()
    {


    }
    /// <summary>
    /// HeadUpdate��LastUpdate�̊�<br/>
    /// ���State�����ɓ���<br/>
    /// �p����ŒǋL
    /// </summary>
    protected virtual void MiddleUpdate()
    {

    }


    /// <summary>
    /// Update�̍Ō�<br/>
    /// �p����ŒǋL
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
    /// ���S���̋���<br/>
    /// �p����ŒǋL
    /// </summary>
    protected virtual void Death()
    {
        Destroy(gameObject);
    }

}