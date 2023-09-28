using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtenemy : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    void Update()
    {
        // 対象物へのベクトルを算出
        Vector3 toDirection = target.transform.position - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }
}
