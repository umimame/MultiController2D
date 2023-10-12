using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator‚ð“Z‚ß‚éƒNƒ‰ƒX
/// </summary>
[Serializable] public class Hunger : MonoBehaviour
{
    [field: SerializeField] public Generator inUse { get; set; }
    [field: SerializeField] private List<BulletGenerator> bGenerators { get; set; } = new List<BulletGenerator>();
    [field: SerializeField] private List<Generator> generators { get; set; } = new List<Generator>();

    public void Start()
    {
        foreach (Generator generator in generators)
        {
            generator.Initialize();
            generator.parent = gameObject;
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