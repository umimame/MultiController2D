using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [field: SerializeField] public bool exe { get; set; }
    [SerializeField] private GameObject bullet;
    [SerializeField] private Interval rate;

    public virtual void Start()
    {
        rate.Initialize(true);
    }

    public virtual void Update()
    {
        if (exe == true)
        {
            rate.Launch(Generate);
        }

        rate.Update();
    }
    public virtual void Generate()
    {
        GameObject clone = Instantiate(bullet);
        clone.transform.position = transform.position;
        rate.Reset();
    }
}

[Serializable] public class Hunger
{
    [field: SerializeField] public Generator inUse { get; set; }
    [field: SerializeField] private List<Generator> generators { get; set; } = new List<Generator>();

    public void Initialize()
    {
        foreach(Generator generator in generators)
        {
            Debug.Log(generator);
        }
    }

    public void Update()
    {
        foreach (Generator generator in generators)
        {
            generator.Update();
        }
    }
}