using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PresetByPlayerType",menuName = "ScriptableObject/PresetByPlayerType")]
[Serializable] public class PresetsByPlayerType : ScriptableObject
{
    [field: SerializeField] public int playerType;
    [field: SerializeField] public List<Color> colorPre { get; set; } = new List<Color>();
    [field: SerializeField] public List<Color> playerColorPre { get; set; } = new List<Color>();
    [field: SerializeField] public List<Rect> cameraRectPre { get; set; } = new List<Rect>();
    [field: SerializeField] public List<Vector2> playerPos { get; set; } = new List<Vector2>();
    public void Initialize()
    {
        playerType = World.instance.PlayerTypes;
        colorPre = new List<Color>(playerType);
        cameraRectPre = new List<Rect>(playerType);
        for(int i = 0; i < playerType; ++i)
        {
        }
    }
}
