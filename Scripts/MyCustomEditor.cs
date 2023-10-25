using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class MyCustomEditor : Editor
{
    /// <summary>
    /// Boolチェックボックスにより出現するObjectField
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="check"></param>
    public void ObjFieldByBool(UnityEngine.Object obj, bool check, Type type)
    {
        if (check)
        {
            obj = EditorGUILayout.ObjectField(nameof(obj), obj, type, true);
        }
    }
    public void ObjFieldByBool(GameObject obj, bool check)
    {
        if (check)
        {
            obj = (GameObject)EditorGUILayout.ObjectField(nameof(obj), obj, typeof(GameObject), true);
        }
        

    }

    public void ObjField(UnityEngine.Object obj, Type type)
    {
        obj = EditorGUILayout.ObjectField(nameof(obj), obj, type, true);
    }
    public void ObjField(UnityEngine.Object obj)
    {
        obj = EditorGUILayout.ObjectField(nameof(obj), obj, typeof(GameObject), true);
    }

    public void Label(string value, string labelName = "Value")
    {
        EditorGUILayout.LabelField(labelName, value);
    }


}
