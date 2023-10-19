using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [field: SerializeField] public PlayerInput input{get; private set; }
    [field: SerializeField] public CircleClamp clamp { get; set; }
    [field: SerializeField] public Hunger hunger { get; set; }
    [field: SerializeField] public bool ready { get; set; }
    private void Awake()
    {
        transform.tag = transform.parent.tag;   // tag��e�I�u�W�F�N�g�Ɠ����ɂ���

    }
    protected override void Start()
    {
        base.Start();
        transform.tag = transform.parent.tag;
        keyMap = new KeyMap();
        keyMap.Enable();
        DeviceSeach();
        beforeVec = Vector3.zero;
        clamp.Initialize();
        hunger = GetComponent<Hunger>();
    }

    /// <summary>
    /// override���Update�̍ŏ��ɋL�q
    /// </summary>
    protected override void HeadUpdate()
    {
        base.HeadUpdate();
        InputToVelocityPlan();
    }

    protected override void MiddleUpdate()
    {
        base.MiddleUpdate();
    }

    protected override void LastUpdate()
    {
        base.LastUpdate();
        clamp.Limit();
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
    protected virtual void InputToVelocityPlan()
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

    protected virtual void UnderAttack(Collider2D co)
    {
        if(co.tag != transform.tag) // �Փː�̃^�O�����R�ƈقȂ�ꍇ
        {
            Debug.Log("UnderAttack");
            if (co.GetComponent<Bullet>())
            {
                Bullet coScript = co.GetComponent<Bullet>();
                hp.entity -= coScript.pow.entity;
                Debug.Log(hp.entity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnderAttack(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        UnderAttack(collision);
    }

    protected override void Death()
    {
        engine.sprite.enabled = false;
        engine.aimCircle.enabled = false;
    }
    protected virtual float Attack1
    {
        get { return keyMap.Keybord.Attack1.ReadValue<float>(); }
    }

    public void ColorChange(Color color)
    {
        engine.sprite.color = color;
        engine.aimCircle.color = new Color(color.r, color.g, color.b, 0.5f);
        
    }
}
