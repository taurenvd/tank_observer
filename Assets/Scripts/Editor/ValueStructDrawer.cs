using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Field changes in Inspector, but no OnValueChanged comes because no access to "Value" property setter. 
/// Unlike ScriptableValuesEditor where access via Reflection is granted;
/// </summary>
public abstract class ValueStructDrawer : PropertyDrawer
{
    SerializedProperty m_value;
    SerializedProperty use_scriptable;
    SerializedProperty is_const;
    SerializedProperty scriptable_value;
    SerializedProperty OnValueChanged;

    static float padding = 10;

    bool show_event;
    bool is_init;
    bool show_editor;

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        if (!is_init || m_value == null)
        {
            OnEnable(property);
        }

        var new_rect = new Rect(pos);

        new_rect.width = pos.width / 2f;
        new_rect.height = 16;
        
        new_rect.x += new_rect.width + padding;
        new_rect.width = 75;

        EditorGUI.BeginDisabledGroup(is_const.boolValue);

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(new_rect, m_value, GUIContent.none);

        if (EditorGUI.EndChangeCheck())
        {
            //Debug.Log("Property has changed");
        }
        EditorGUI.EndDisabledGroup();
        new_rect.x += new_rect.width + padding;

        new_rect.width = 50;

        new_rect.x += new_rect.width / 2;

        new_rect.width *= 2;

        is_const.boolValue = GUI.Toggle(new_rect, is_const.boolValue, "Const?");

        var foldout_rect = new Rect(pos.x, pos.y, pos.width, 20);

        show_editor = EditorGUI.Foldout(foldout_rect, show_editor, property.displayName + string.Format(" <{0}>", m_value.type));

        if (show_editor)
        {
            new_rect.y = pos.y + 20;
            new_rect.x = 30;

            if (GUI.Toggle(new_rect, use_scriptable.boolValue, "Import?"))
            {
                use_scriptable.boolValue = true;
                new_rect.x += new_rect.width;
                new_rect.width = 150;
                EditorGUI.PropertyField(new_rect, scriptable_value, GUIContent.none);
            }
            else
            {
                use_scriptable.boolValue = false;
            }

            var event_pos = new Rect(pos.width / 2f, pos.y + 20, pos.width / 2f, 30);

            new_rect.y += 20;
            new_rect.x = 30;
            show_event = GUI.Toggle(new_rect, show_event, "Show Event?");

            if (show_event)
            {                             
                EditorGUI.PropertyField(event_pos, OnValueChanged);
            }
        }
    }
    void OnEnable(SerializedProperty property)
    {
        m_value = property.FindPropertyRelative("m_value");
        is_const = property.FindPropertyRelative("is_const");
        use_scriptable = property.FindPropertyRelative("use_scriptable");
        scriptable_value = property.FindPropertyRelative("scriptable_value");
        OnValueChanged = property.FindPropertyRelative("OnValueChanged");

        is_init = true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (show_editor ? 60 : 40) + (show_event&&show_editor ? EditorGUI.GetPropertyHeight(OnValueChanged) : 0);
    }
}


[CustomPropertyDrawer(typeof(FloatValueClass))]
public class FloatValueClassDrawer : ValueStructDrawer { }

[CustomPropertyDrawer(typeof(IntValueClass))]
public class IntValueClassDrawer : ValueStructDrawer { }

[CustomPropertyDrawer(typeof(BoolValueClass))]
public class BoolValueClassDrawer : ValueStructDrawer { }

[CustomPropertyDrawer(typeof(StringValueClass))]
public class StringValueClassDrawer : ValueStructDrawer { }

[CustomPropertyDrawer(typeof(Vector2ValueClass))]
public class Vector2ValueClassDrawer : ValueStructDrawer { }
