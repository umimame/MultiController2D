using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using My;
/// <summary>
/// これを継承して各コントローラーの動作を記述する<br/>
/// InputToVelocityを基本動作に上書きする
/// </summary>
public class PlayerController : Chara
{
    [SerializeField] private BarByParam hpBar;
    [field: SerializeField] public Parameter en;
    [SerializeField] private BarByParam enBar;
    [field: SerializeField] public Vector2 beforeVec { get; set; }
    [field: SerializeField] public Vector3 inputVelocityPlan { get; set; }
    public KeyMap keyMap { get; private set; }
    [field: SerializeField] public PlayerInput input{get; private set; }
    [field: SerializeField] public CircleClamp clamp { get; set; }
    [field: SerializeField] public Hunger hunger { get; set; }
    [field: SerializeField] public bool ready { get; set; }
    [field: SerializeField] public Shaker cameraShaker { get; set; }
    [SerializeField] private Shaker spriteShaker;
    [SerializeField] private Instancer deathEffect;
    [SerializeField] private AudioClip deathSound;
    [field: SerializeField] public bool alive { get; set; }
    private void Awake()
    {
        transform.tag = transform.parent.tag;   // tagを親オブジェクトと同じにする

    }
    protected override void Start()
    {
        base.Start();
        Debug.Log("Initialize");
        en.Initialize();
        transform.tag = transform.parent.tag;
        keyMap = new KeyMap();
        //keyMap.Enable();
        DeviceSeach();
        beforeVec = Vector3.zero;
        clamp.Initialize();
        hunger = GetComponent<Hunger>();
    }

    /// <summary>
    /// override後のUpdateの最初に記述
    /// </summary>
    protected override void HeadUpdate()
    {
        base.HeadUpdate();
        if (hp.entity <= 0)
        {
            state = State.Death;
            alive = false;
        }
        else
        {
            alive = true;
        }

        deathEffect.Update();
        if (alive)
        {
            InputToVelocityPlan();
        }
        hpBar.Update(hp);
        if (alive)
        {
            en.Update();
            enBar.Update(en);
        }
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
    protected virtual void InputToVelocityPlan()
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

    protected virtual void UnderAttack(Collider2D co)   // 衝突時
    {
        if(co.tag != transform.tag) // 衝突先のタグが自軍と異なる場合
        {
            if (co.GetComponent<Bullet>())
            {
                Bullet coScript = co.GetComponent<Bullet>();
                hp.entity -= coScript.pow.entity;
            }

            cameraShaker.active = true; // 衝突時の衝撃演出
            spriteShaker.active = true;
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
        if(deathEffect.state != Instancer.DisplayState.Death && deathEffect.Displaying == false)
        {

            deathEffect.Instance(gameObject);
            World.instance.audioSource.PlayOneShot(deathSound);
        }
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
