using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtenemy : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    void Update()
    {
        // �Ώە��ւ̃x�N�g�����Z�o
        Vector3 toDirection = target.transform.position - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }
}
