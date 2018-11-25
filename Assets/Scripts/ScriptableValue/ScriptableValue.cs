using UnityEngine;


public abstract class ScriptableValue<T> : ScriptableObject
{
    [SerializeField] T m_value= default(T);

    public event System.Action<T> OnValueChanged;

    public T Value
    {
        get
        {
            return m_value;
        }

        set
        {
            m_value = value;
            OnValueChanged?.Invoke(m_value);
        }
    }

    public static implicit operator T(ScriptableValue<T> s_object)
    {
        return s_object.Value;
    }

}

