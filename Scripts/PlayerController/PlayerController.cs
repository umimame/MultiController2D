using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// これを継承して各コントローラーの動作を記述する<br/>
/// InputToVelocityを基本動作に上書きする
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
    /// override後のUpdateの最初に記述
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
            Debug.Log("アクティブなプレイヤーではありません");
            return;
        }
        Debug.Log("Player" + input.user.id);
        foreach (var device in input.devices)
        {
            Debug.Log(device);
        }
    }
    /// <summary>
    /// 継承先でoverrideして使う
    /// </summary>
    protected virtual void InputToVelocity()
    {
        // 継承先でoverrideして使う
    }

    protected virtual bool Inputting
    {
        get { return true; }
    }

    /// <summary>
    /// 移動の入力
    /// </summary>
    protected virtual Vector3 Move
    {
        get { return Vector3.zero; }
    }
}
