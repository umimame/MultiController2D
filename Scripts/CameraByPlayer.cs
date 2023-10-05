using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アタッチ先のカメラはPlayerの子オブジェクトにすること
/// </summary>
public class CameraByPlayer : MonoBehaviour
{
    [SerializeField] private GameObject body;

    private void Update()
    {
        transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -10);
    }
}
