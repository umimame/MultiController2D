using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator‚ð“Z‚ß‚éƒNƒ‰ƒX
/// </summary>
[Serializable] public class Hunger : MonoBehaviour
{
    [field: SerializeField] public BulletGenerator inUse { get; set; }
    [field: SerializeField] private List<BulletGenerator> generators { get; set; } = new List<BulletGenerator>();

    public void Start()
    {
        foreach (BulletGenerator generator in generators)
        {
        }
        inUse = generators[0];
    }

    public void Update()
    {
        foreach (BulletGenerator generator in generators)
        {
        }
    }
}