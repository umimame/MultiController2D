using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 対象のオブジェクトで移動範囲を制限する
/// </summary>
public class ClampByStage : MonoBehaviour
{
    [field: SerializeField] public GameObject stage { get; set; }

    private void Start()
    {
            
    }
    private void Update()
    {
        Vector2 clamp;
        clamp.x = Mathf.Clamp(transform.position.x, stage.transform.position.x - stage.transform.localScale.x / 2, stage.transform.position.x + stage.transform.localScale.x / 2);
        clamp.y = Mathf.Clamp(transform.transform.position.y, stage.transform.position.y - stage.transform.localScale.y / 2, stage.transform.position.y + stage.transform.localScale.y / 2);
        transform.position = clamp;
    }

    public void Set()
    {

    }
}
