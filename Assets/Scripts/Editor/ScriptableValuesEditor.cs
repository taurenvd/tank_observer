using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class ScriptableValuesEditor<T> : Editor
{
    SerializedProperty value;
    PropertyInfo info;

    static readonly Dictionary<Type, Func<Type,SerializedProperty, object>> TypeToSerializedType =
    new Dictionary<Type, Func<Type,SerializedProperty, object>>()
    {
            { typeof(int), (type,value) => value.intValue },
            { typeof(float), (type,value) => value.floatValue },
            { typeof(string), (type,value) => value.stringValue },
            { typeof(bool), (type,value) => value.boolValue },
            { typeof(Vector2), (type,value) => value.vector2Value },
            { typeof(Vector3), (type,value) => value.vector3Value },
            { typeof(Bounds), (type,value) => value.boundsValue },
            { typeof(Rect), (type,value) => value.rectValue }
    };

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Label("Custom Editor: \n");

        EditorGUI.BeginChangeCheck();


        EditorGUILayout.PropertyField(value);


        if (EditorGUI.EndChangeCheck())
        {
            SetValue(typeof(T));

        }

        serializedObject.ApplyModifiedProperties();
    }

    void OnEnable()
    {
        value = serializedObject.FindProperty("m_value");
        info = target.GetType().GetProperty("Value");
    }

    void SetValue(Type type)
    {
        if (TypeToSerializedType.ContainsKey(type))
        {

        info.SetValue(target, TypeToSerializedType[type].Invoke(type, value));
        }
        else
        {
            throw new NotImplementedException();
        }
        
    }
}
[CustomEditor(typeof(FloatValue))]
public class FloatValueEditor : ScriptableValuesEditor<float> { }


[CustomEditor(typeof(IntValue))]
public class IntValueEditor : ScriptableValuesEditor<int> { }

[CustomEditor(typeof(StringValue))]
public class StringValueEditor : ScriptableValuesEditor<string> { }

[CustomEditor(typeof(BoolValue))]
public class BoolValueEditor : ScriptableValuesEditor<bool> { }