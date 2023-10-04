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
    protected override void Start()
    {
        base.Start();
        keyMap = new KeyMap();
        keyMap.Enable();
        DeviceSeach();
        beforeVec = Vector3.zero;
    }

    protected override void Update()
    {
        base.Update();
        InputToVelocity();

        engine.VelocityResult();


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
    protected virtual Vector2 Move
    {
        get { return Vector2.zero; }
    }

    //private void KeyMapAdd()
    //{
    //    keyMap.Keybord.Move.started += OnMove;
    //    keyMap.Keybord.Move.performed += OnMove;
    //    keyMap.Keybord.Move.canceled += OnMove;

    //    keyMap.Pad.Move.started += OnMove;
    //    keyMap.Pad.Move.performed += OnMove;
    //    keyMap.Pad.Move.canceled += OnMove;

    //}

    //public void OnMove(InputAction.CallbackContext value)
    //{
    //    engine.velocityPlan += value.ReadValue<Vector2>().normalized * speed.entity;
    //}
}
