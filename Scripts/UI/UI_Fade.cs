using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using UnityEngine.UIElements;
using System;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 空のCanvasにつけてPrefab化して使用
/// </summary>
public class UI_Fade : SingletonDontDestroy<UI_Fade>
{
    [field: SerializeField] public FadeParameter inObj;
    [field: SerializeField] public FadeParameter outObj;
    private List<FadeParameter> faders = new List<FadeParameter>();
    [field: SerializeField] public SceneAsset scene { get; set; }

    public void Start()
    {
        TypeFinder finder = gameObject.AddComponent<TypeFinder>();
        faders = finder.GetAndInList<FadeParameter>(GetType());
        Destroy(finder);

        inObj.Initialize(gameObject, 0.0f, outObj, scene);
        outObj.Initialize(scene, gameObject, 1.0f);
        
        if (inObj.obj.Obj != null)  // inObjから優先的に実行
        {
            inObj.Instance();
            return;
        }

        outObj.Instance();  // inObjが無ければ実行

    }

    public void Update()
    {
        foreach(FadeParameter fade in faders)
        {
            fade.Update();
            if(fade.obj.state != Instancer.DisplayState.Death) { return; }
        }

        Destroy(gameObject);    // 全て出現したなら

    }

    //#if UNITY_EDITOR
    //    [CustomEditor(typeof(UI_Fade))]

    //    public class UI_FadeEditor : MyCustomEditor
    //    {
    //        private SerializedProperty property;
    //        private void OnEnable()
    //        {
    //            property = serializedObject.FindProperty("UI_Fade");
    //        }
    //        public override void OnInspectorGUI()
    //        {
    //            UI_Fade script = (UI_Fade)target;
    //            script.useIn = EditorGUILayout.Toggle("Fade In", script.useIn);
    //            if (script.useIn)
    //            {
    //                ObjField(script.inObject);
    //                Label(script.deathInObject.ToString(), nameof(script.deathInObject));
    //            }

    //            script.useOut = EditorGUILayout.Toggle("Fade Out", script.useOut );
    //            if (script.useOut)
    //            {
    //                ObjField(script.outObject);
    //                Label(script.deathOutObject.ToString(), nameof(script.deathOutObject));
    //            }
    //            if (GUI.changed == true)
    //            {
    //                EditorUtility.SetDirty(script);
    //            }

    //        }
    //    }
    //#endif
}


[Serializable] public class FadeParameter
{
    public enum FadeType
    {
        In,
        Connect,
        Out,
        End,
    }
    [field: SerializeField] public Instancer obj { get; set; }
    [field: SerializeField] public SpriteOrImage img { get; set; } = new SpriteOrImage();
    [field: SerializeField] public float speed { get; set; }
    [field: SerializeField] public FadeType type{ get; set; }
    [field: SerializeField] public GameObject parent { get; set; }
    private FadeParameter afterFader;
    private SceneAsset afterScene;
    private SceneAsset concurrentScene;
    private float initialAlpha;

    public void Initialize(GameObject parent, float alpha)
    {
        this.parent = parent;
        obj.Initialize();
        initialAlpha = alpha;

    }
    public void Initialize(SceneAsset concurrentScene, GameObject parent, float alpha)
    {
        this.concurrentScene = concurrentScene;
        this.parent = parent;
        obj.Initialize();
        initialAlpha = alpha;

    }
    public void Initialize(GameObject parent, float alpha, FadeParameter after = null, SceneAsset afterScene = null)
    {
        this.afterScene = afterScene;
        this.parent = parent;
        obj.Initialize();
        initialAlpha = alpha;

        if (after != null) { this.afterFader = after; }
    }

    public void Instance()
    {
        if (concurrentScene != null) {

            SceneManager.LoadScene(concurrentScene.name); }
        if(obj.Obj == null) { return; }
        parent.transform.SetAsLastSibling();
        obj.Instance(parent);
        img.Initialize(obj.Last);
        img.Alpha = initialAlpha;
    }

    /// <summary>
    /// Instanceが実行されているなら処理される
    /// </summary>
    public void Update()
    {
        obj.Update();
        if (obj.state != Instancer.DisplayState.NotDisplayYet) { Fade(); }
    }

    public void Fade()
    {
        switch(type)
        {
            case FadeType.In:
                img.Alpha += speed;
                if(img.Alpha >= 1.0f) {
                    GameObject.Destroy(obj.Last);
                    Next();
                    type = FadeType.End;
                }
                break;
            case FadeType.Connect:
                break;
                case FadeType.Out:
                img.Alpha -= speed;
                if (img.Alpha <= 0.0f) {

                    GameObject.Destroy(obj.Last);
                    Next();
                    type = FadeType.End;
                }
                break;
        }
    }

    public void Next()
    {
        if(afterFader != null)
        {
            afterFader.Instance();
        }

        if (afterScene != null) 
        { 
            SceneManager.LoadScene(afterScene.name); 
        }
    }


}

[Serializable] public class FadeInstancer : Instancer
{
    [field: SerializeField] public UI_Fade fadeUI;
    [field: SerializeField] public SceneAsset scene;

    public void Initialize()
    {
        fadeUI = Obj.GetComponent<UI_Fade>();
        fadeUI.scene = scene;
    }

}
