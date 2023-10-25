using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 自動で削除されるObjectにのみ使用
/// </summary>
[Serializable] public class Instancer
{
    public enum DisplayState
    {
        NotDisplayYet,
        Displaying,
        Death,
    }
    [field: SerializeField] public GameObject Obj { get; set; }
    [field: SerializeField] public ParticleSystem particle { get; set; }
    [field: SerializeField] public List<GameObject> clones { get; set; } = new List<GameObject>();
    [field: SerializeField] public DisplayState state { get; private set;}

    /// <summary>
    /// Initialize時に指定した場合、親子関係が作られる
    /// </summary>
    [field: SerializeField] public Instancer afterObj;

    public virtual void Initialize(Instancer afterObj = null)
    {
        state = DisplayState.NotDisplayYet;
        if(afterObj != null) { this.afterObj = afterObj; }
    }

    public virtual void Update()
    {
        foreach(GameObject clone in clones) 
        {
            if (clone != null) { state = DisplayState.Displaying; }// 一つでも表示中なら
            
        }

        switch (state)
        {
            case DisplayState.NotDisplayYet:
                break;

            case DisplayState.Displaying:
                clones.RemoveAll(value => value == null);
                if (clones.Count == 0) { state = DisplayState.Death; }
                break;

            case DisplayState.Death:
                break;
        }
    }
    public virtual void Instance()
    {
        clones.Add(GameObject.Instantiate(Obj));
        if(particle != null)
        {
            GameObject.Instantiate(particle);
        }
    }
    public virtual void Instance(GameObject parent)
    {
        clones.Add(GameObject.Instantiate(Obj, parent.transform));
        if (particle != null)
        {
            GameObject.Instantiate(particle,parent.transform);
        }
    }

    /// <summary>
    /// clonesの最後を渡す
    /// </summary>
    public GameObject Last
    {
        get { return clones[clones.Count - 1]; }
    }

    public bool Displaying
    {
        get
        {
            switch (state)
            {
                case DisplayState.NotDisplayYet:
                    return false;

                case DisplayState.Displaying:
                    return true;

                case DisplayState.Death:
                    return false;
            }

            return false;
        }
    }

}
