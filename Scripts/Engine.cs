using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// オブジェクトを動かす基本クラス
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[Serializable] public class Engine : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D rb { get; set; }
    [field: SerializeField] public Vector3 velocityPlan { get; set; }
    [field: SerializeField] public Collider2D coll { get; set; }
    [field: SerializeField] public SpriteRenderer sprite { get; set; }
    [field: SerializeField] public float time { get; set; }
    private float angle;
    private float difference;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocityPlan = new Vector2(0.0f, 0.0f);
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        time = 0.0f;

        float angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
        float difference = transform.eulerAngles.z - angle;
    }
    private void Update()
    {
        
    }

    /// <summary>
    /// アタッチしたオブジェクトのUpdatenoの最後に呼ぶ
    /// </summary>
    public void VelocityResult()
    {
        angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
        difference = transform.eulerAngles.z - angle;
        rb.velocity = velocityPlan;
        
    }
    public void LookAtVec(Vector3 obj)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, obj - transform.position);
    }

}