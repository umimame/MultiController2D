using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(Mathf.Atan2(v.x, v.y));
        return Mathf.Repeat(Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg, 360);
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
}
