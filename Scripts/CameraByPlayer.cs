using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アタッチ先のカメラはPlayerの子オブジェクトにすること
/// </summary>
public class CameraByPlayer : MonoBehaviour
{
    [SerializeField] private Chara body;
    private Camera cam;
    [SerializeField] private CameraPreset camPreset1;
    [SerializeField] private CameraPreset camPreset2;
    private void Start()
    {

        cam = GetComponent<Camera>();

        // マウス操作側にメインカメラを設定
        if (body.GetComponent<KeybordMouse>())
        {
            transform.tag = "MainCamera";
        }

        // 親オブジェクトのタグを基に画面分割
        switch (transform.parent.tag)
        {
            case "Player01":
                camPreset1.Set(cam);
                break;
            case "Player02":
                camPreset2.Set(cam);
                break;
        }
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
/// プレイヤー毎にカメラを設定できる
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