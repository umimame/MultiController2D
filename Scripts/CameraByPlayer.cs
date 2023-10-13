using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�^�b�`��̃J������Player�̎q�I�u�W�F�N�g�ɂ��邱��
/// </summary>
public class CameraByPlayer : MonoBehaviour
{
    [SerializeField] private Chara body;
    private Camera cam;
    [SerializeField] private PresetByPlayerType<CameraPreset> camPre;
    private void Start()
    {

        cam = GetComponent<Camera>();

        // �}�E�X���쑤�Ƀ��C���J������ݒ�
        if (body.GetComponent<KeybordMouse>())
        {
            transform.tag = "MainCamera";
        }

        // �e�I�u�W�F�N�g�̃^�O����ɉ�ʕ���
        cam.backgroundColor = World.instance.presets.colorPre[AddFunction.TagToArray(body.tag)];
        cam.rect = World.instance.presets.cameraRectPre[AddFunction.TagToArray(body.tag)];
    }

    private void Update()
    {
        if (body.state != Chara.State.Death)
        {
            transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -10);
        }
    }
}

/// <summary>
/// �v���C���[���ɃJ������ݒ�ł���
/// </summary>
[Serializable] public class CameraPreset
{
    [SerializeField] private Rect rect;
    [SerializeField] private Color backGroundColor;

    public void Set(Camera cam)
    {
        cam.rect = rect;
        cam.backgroundColor = backGroundColor;
    }
}