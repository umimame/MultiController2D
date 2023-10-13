
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class AlwaysUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [field: SerializeField] public bool display { get; set; }
    [field: SerializeField] public float time { get; private set; }

    private void Start()
    {
        time = 0.0f;
    }

    private void Update()
    {

    }

    public void Display()
    {
        display = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        display = false;
        gameObject.SetActive(false);
    }

}