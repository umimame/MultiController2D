using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�^�b�`��̃J������Player�̎q�I�u�W�F�N�g�ɂ��邱��
/// </summary>
public class CameraByPlayer : MonoBehaviour
{
    [SerializeField] private GameObject body;

    private void Update()
    {
        transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -10);
    }
}
