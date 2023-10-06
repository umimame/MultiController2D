using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ������p�����Ċe�R���g���[���[�̓�����L�q����<br/>
/// InputToVelocity����{����ɏ㏑������
/// </summary>
public class PlayerController : Chara
{
    [field: SerializeField] public Vector2 beforeVec { get; set; }
    public KeyMap keyMap { get; private set; }
    [SerializeField] private PlayerInput input;
    [SerializeField] private CircleClamp clamp;
    protected override void Start()
    {
        base.Start();
        keyMap = new KeyMap();
        keyMap.Enable();
        DeviceSeach();
        beforeVec = Vector3.zero;
        clamp.Initialize();
    }

    /// <summary>
    /// override���Update�̍ŏ��ɋL�q
    /// </summary>
    protected override void HeadUpdate()
    {
        base.HeadUpdate();
        InputToVelocity();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void DeviceSeach()
    {
        if (!input.user.valid)
        {
            Debug.Log("�A�N�e�B�u�ȃv���C���[�ł͂���܂���");
            return;
        }
        Debug.Log("Player" + input.user.id);
        foreach (var device in input.devices)
        {
            Debug.Log(device);
        }
    }
    /// <summary>
    /// �p�����override���Ďg��
    /// </summary>
    protected virtual void InputToVelocity()
    {
        // �p�����override���Ďg��
    }

    protected virtual bool Inputting
    {
        get { return true; }
    }

    /// <summary>
    /// �ړ��̓���
    /// </summary>
    protected virtual Vector3 Move
    {
        get { return Vector3.zero; }
    }
}
