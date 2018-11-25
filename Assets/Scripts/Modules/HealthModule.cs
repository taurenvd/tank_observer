using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    #region Fields

    [Space]
    public FloatValueClass m_max_health;
    public FloatValueClass m_health;
    public FloatValueClass m_health01;
    public FloatValueClass m_defence;
    #endregion

    #region Properties

    public float Defence
    {
        get
        {
            return m_defence;
        }
    }



    #endregion

    void Start()
    {
        if (m_defence == 0)
        {
            m_defence.Value = 0.1f;
        }

        m_health.OnValueChanged.AddListener(hp =>
        {
            m_health01.Value = m_health / m_max_health;
        });

#if UNITY_EDITOR

        if (m_max_health == 0)
        {
            Debug.Log("Max health not specified", gameObject);
        }
#endif
        m_health.Value = m_max_health;

    }
}
