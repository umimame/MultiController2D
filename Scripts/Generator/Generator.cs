using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "ScriptableObject/Generator")]
[Serializable]
public class Generator : ScriptableObject
{

    [field: SerializeField] public bool trigger { get; set; }
    [SerializeField] private GameObject bullet;
    [SerializeField] private Interval rate;
    [field: SerializeField] public Vector3 offset { get; set; }
    [field: SerializeField] public GameObject parent { get; set; }
    [field: SerializeField] public PresetByPlayerType<Vector3> offsets { get; set; }

    public virtual void Initialize()
    {
        rate.Initialize(true);
        offsets.Initialize();
    }

    public virtual void Update()
    {
        if (trigger == true)
        {
            rate.Launch(Generate);
        }

        rate.Update();
    }

    /// <summary>
    /// bulletの生成と位置・タグ補正
    /// </summary>
    public virtual void Generate()
    {
        GameObject clone = Instantiate(bullet);
        Bullet cloneScript = clone.GetComponent<Bullet>();
        cloneScript.engine.transform.rotation = parent.transform.rotation;
        clone.transform.position = parent.transform.rotation * offset + parent.transform.position;
        clone.tag = parent.tag; // 弾のTagをparentと同じにする
        cloneScript.engine.transform.tag = parent.tag;
        rate.Reset();
    }
}
