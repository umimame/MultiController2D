using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[Serializable] public class Engine : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D rb { get; set; }
    [field: SerializeField] public Vector2 velocityPlan { get; set; }
    [field: SerializeField] public Collider2D col { get; set; }
    [field: SerializeField] public SpriteRenderer sprite { get; set; }
    [field: SerializeField] public float time { get; set; }
    private float angle;
    private float difference;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocityPlan = new Vector2(0.0f, 0.0f);
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        time = 0.0f;

        float angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
        float difference = transform.eulerAngles.z - angle;
    }

    public void VelocityResult()
    {
        angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
        difference = transform.eulerAngles.z - angle;
        //rb.velocity = velocityPlan;
        if (angle > 0.0f)
        {
            PlusCurve();
        }
        else if(angle < 0.0f)
        {
            MinusCurve();
        }
        else if(angle == 0.0f)
        {
            if(transform.eulerAngles.z <= 180.0f && 0.0f <= transform.eulerAngles.z)
            {
                PlusCurve();
            }
            else if(0.0f < transform.eulerAngles.z) { MinusCurve(); }
        }
        Debug.Log(angle);
    }

    private void PlusCurve()
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0.0f, 180.0f, angle), Mathf.Abs(difference) / 2.0f);
    }

    private void MinusCurve()
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0.0f, 180.0f, -angle * 2), Mathf.Abs(difference) / 2.0f);
    }

}