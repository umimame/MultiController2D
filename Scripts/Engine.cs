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
    [SerializeField] private GameObject lookTarget;

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
    private void Update()
    {
        
    }

    public void VelocityResult()
    {
        angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
        difference = transform.eulerAngles.z - angle;
        //rb.velocity = velocityPlan;
        //if (angle > 0.0f)
        //{
        //    PlusCurve();
        //}
        //else if(angle < 0.0f)
        //{

        //}
        //else if(angle == 0.0f && KeyMapCallBack.Inputting == true)
        //{
        //    if(transform.eulerAngles.z < 180.0f)
        //    {
        //        MinusCurve();
        //    }
        //    else if(transform.eulerAngles.z >= 180.0f)
        //    {
        //        PlusCurve();
        //    }
        //}
        Debug.Log(angle);
        //Quaternion look = Quaternion.LookRotation(transform.position - lookTarget.transform.position, Vector3.up);
        //transform.rotation = look * Quaternion.FromToRotation(-Vector3.forward, Vector3.forward);
        transform.LookAt(lookTarget.transform, Vector3.forward);
        
    }

    private void PlusCurve()
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0.0f, 0.0f, angle), Mathf.Abs(difference) / 2.0f);
        Debug.Log("Plus");
    }

    private void MinusCurve()
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0.0f, 0.0f, -angle * 2), Mathf.Abs(difference) / 2.0f);
        Debug.Log("Minus");
    }

}