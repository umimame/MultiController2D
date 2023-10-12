using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class World : MonoBehaviour
{
    public static KeyMapCallBack keyMap;
    public static int controllerType;
    private void Start()
    {
        keyMap = new KeyMapCallBack();
        controllerType = 2;
    }
}

public static class AddFunction
{

    /// <summary>
    /// Vector2を角度(360度)に変更
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float Vec2ToAngle(Vector2 v)
    {
        return Mathf.Repeat(Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg, 360);
    }

    public static float GetAngleByVec2(Vector3 start, Vector3 target)
    {
        float angle;
        Vector3 dt = start - target;
        angle = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

        return angle;
    }

    public static Vector3 CameraToMouse()
    {
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
    }
}

public class KeyMapCallBack
{
    public static KeyMap keyMap;

    public KeyMapCallBack()
    {
        keyMap = new KeyMap();
        keyMap.Enable();
    }

    public static Vector2 Move{ get { return keyMap.Keybord.Move.ReadValue<Vector2>(); } }
    public static bool Inputting
    {
        get
        {
            if (Move == new Vector2(0, 0))
            {
                return false;
            }
            return true;
        }
    }

    public static void Schema()
    {
        foreach(var device in InputSystem.devices)
        {
            Debug.Log(device);
        }
    }
}
/// <summary>
/// 移動範囲を円形に制限
/// </summary>
[Serializable] public class CircleClamp
{

    [SerializeField] private GameObject center;
    [field: SerializeField] public GameObject moveObject { get; set; }
    [SerializeField] private float radius;

    public void Initialize()
    {
        moveObject.transform.position = center.transform.position;
    }
    public void Limit()
    {
        if(Vector2.Distance(moveObject.transform.position, center.transform.position) > radius)
        {
            Vector3 nor = moveObject.transform.position - center.transform.position;
            moveObject.transform.position = center.transform.position + nor.normalized * radius;
        }
    }
}

/// <summary>
/// 間隔制御クラス
/// </summary>
[Serializable] public class Interval
{
    [field: SerializeField] public bool active { get; private set; }
    [SerializeField] private float interval;
    [SerializeField] private float time;

    /// <summary>
    /// 引数には最初から使用できるかどうかを記述する
    /// </summary>
    /// <param name="start"></param>
    public void Initialize(bool start)
    {
        if(start == true)
        {
            time = interval;
        }
        else
        {
            time = 0.0f;
        }
        active = (time >= interval) ? true : false;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if(time >= interval)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    /// <summary>
    /// 引数は「引数無しのメソッド」
    /// </summary>
    /// <param name="action"></param>
    public void Launch(Action action)
    {
        if(active == true)
        {

            action();
        }
    }

    public void Reset()
    {
        time = 0.0f;
    }
}