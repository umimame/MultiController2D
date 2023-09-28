using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Chara
{
    private KeyMap keyMap;
    [SerializeField] private PlayerInput input;
    protected override void Start()
    {
        base.Start();
        keyMap = new KeyMap();
        keyMap.Enable();
        DeviceSeach();
    }

    protected override void Update()
    {
        base.Update();
        //engine.velocityPlan += keyMap.Pad.Move.ReadValue<Vector2>().normalized * speed.entity;
        InputMove();

        engine.VelocityResult();


    }

    private void DeviceSeach()
    {
        if (!input.user.valid)
        {
            Debug.Log("アクティブなプレイヤーではありません");
            return;
        }
        Debug.Log("Player" + input.user.id);
        foreach (var device in input.devices)
        {
            Debug.Log(device);
        }
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

    private void InputMove()
    {
        if (input.currentControlScheme == "Keybord")
        {
            engine.velocityPlan += keyMap.Keybord.Move.ReadValue<Vector2>().normalized * speed.entity;
        }
        else if(input.currentControlScheme == "Pad")
        {
            engine.velocityPlan += keyMap.Pad.Move.ReadValue<Vector2>().normalized * speed.entity;
        }
    }

    //private void OnMove(InputValue input)
    //{
    //    engine.velocityPlan += input.Get<Vector2>().normalized * speed.entity;
    //}
}
