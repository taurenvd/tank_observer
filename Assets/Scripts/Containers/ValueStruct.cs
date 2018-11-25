using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatTrigger : UnityEvent<float> { }

[System.Serializable]
public class StringTrigger : UnityEvent<string> { }

[System.Serializable]
public class BoolTrigger : UnityEvent<bool> { }

[System.Serializable]
public class IntTrigger : UnityEvent<int> { }

[System.Serializable]
public class Vector2Trigger : UnityEvent<Vector2> { }


[System.Serializable]
public class FloatValueClass : ValueClass<float, FloatValue, FloatTrigger> { }

[System.Serializable]
public class IntValueClass : ValueClass<int, IntValue, IntTrigger> { }

[System.Serializable]
public class BoolValueClass : ValueClass<bool, BoolValue, BoolTrigger> { }

[System.Serializable]
public class StringValueClass : ValueClass<string, StringValue, StringTrigger> { }

[System.Serializable]
public class Vector2ValueClass : ValueClass<Vector2, Vector2Value, Vector2Trigger> { }

public abstract class ValueClass<TType, TScriptableType, TriggerType> where TriggerType : UnityEvent<TType> where TScriptableType : ScriptableValue<TType>
{
    [SerializeField] TType m_value;

    [SerializeField] bool is_const;
    [SerializeField] bool use_scriptable;

    [SerializeField] TScriptableType scriptable_value;

    public TriggerType OnValueChanged;

    public TType Value
    {
        get
        {
            return use_scriptable && scriptable_value ? scriptable_value : m_value;
        }

        set
        {
            if (!is_const)
            {
                m_value = value;


                if (use_scriptable && scriptable_value)
                {
                    scriptable_value.Value = m_value;
                }
                OnValueChanged?.Invoke(m_value);                
                //Debug.Log("Active listners: "+OnValueChanged?.GetInvocationList().Length);
            }
        }
    }

    public static implicit operator TType(ValueClass<TType,TScriptableType,TriggerType> obj)
    {
        return obj.Value;
    }
}