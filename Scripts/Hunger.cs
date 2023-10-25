using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator‚ð“Z‚ß‚éƒNƒ‰ƒX
/// </summary>
[Serializable] public class Hunger : MonoBehaviour
{
    [field: SerializeField] public GameObject parent { get; set; }
    [field: SerializeField] public List<BulletGenerator> inUse { get; set; } = new List<BulletGenerator>();
    [field: SerializeField] private BulletGenerator[] generators { get; set; }

    public void Start()
    {

        generators = parent.GetComponentsInChildren<BulletGenerator>();
        foreach (BulletGenerator generator in generators)
        {
            inUse.Add(generator);
        }
    }

    public void Update()
    {
        foreach (BulletGenerator generator in generators)
        {
        }
    }
}