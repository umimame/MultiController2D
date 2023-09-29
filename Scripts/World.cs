using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class World : MonoBehaviour
{
    public static KeyMapCallBack keyMap;

    private void Start()
    {
        keyMap = new KeyMapCallBack();
    }
}

public static class AddFunction
{

    /// <summary>
    /// Vector2Çäpìx(360ìx)Ç…ïœçX
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

[Serializable] public class CircleClamp
{

    [SerializeField] private GameObject center;
    [SerializeField] private GameObject circle;
    public GameObject moveObject { get; set; }
    [SerializeField] private float radius;

    public void Initialize()
    {
        moveObject = GameObject.Instantiate(circle);
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

[Serializable] public class VectorSave
{
    [SerializeField] private Vector3 beforeVec;

    VectorSave()
    {
        beforeVec = Vector3.zero;
    }

    public bool Moving()
    {
        if(beforeVec == new Vector3(0.0f, 0.0f, 0.0f))
        {
            return false;
        }
        return true;
    }
}