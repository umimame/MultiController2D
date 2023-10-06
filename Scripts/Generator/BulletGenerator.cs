using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [field: SerializeField] public bool trigger { get; set; }
    [SerializeField] private GameObject bullet;
    [SerializeField] private Interval rate;
    [field: SerializeField] public Vector3 offset { get; set; }
    [SerializeField] private GameObject parent;

    public virtual void Start()
    {
        rate.Initialize(true);
    }

    public virtual void Update()
    {
        if (trigger == true)
        {
            rate.Launch(Generate);
        }

        rate.Update();
    }
    public virtual void Generate()
    {
        GameObject clone = Instantiate(bullet);
        Bullet cloneScript = clone.GetComponent<Bullet>();
        cloneScript.engine.transform.rotation = parent.transform.rotation;
        clone.transform.position = parent.transform.rotation * offset + parent.transform.position;
        clone.tag = parent.tag; // ’e‚ÌTag‚ðparent‚Æ“¯‚¶‚É‚·‚é
        cloneScript.engine.transform.tag = parent.tag;
        rate.Reset();
    }
}

[Serializable] public class Hunger
{
    [field: SerializeField] public BulletGenerator inUse { get; set; }
    [field: SerializeField] private List<BulletGenerator> generators { get; set; } = new List<BulletGenerator>();

    public void Initialize()
    {
        foreach(BulletGenerator generator in generators)
        {
        }
    }

    public void Update()
    {
        foreach (BulletGenerator generator in generators)
        {
            generator.Update();
        }
    }
}
