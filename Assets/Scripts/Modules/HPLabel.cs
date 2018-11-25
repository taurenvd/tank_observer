using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

[RequireComponent(typeof(TextMeshPro))]
public class HPLabel : MonoBehaviour
{
    [SerializeField, Space] TextMeshPro m_world_text;

    [SerializeField, Space] HealthModule m_target_hp;

    [SerializeField, Space] Camera m_target_cam;

    [Space]
    [SerializeField] string m_output_format = "{0}";
    [SerializeField] string m_death_msg = "Dead!";

    [SerializeField,Space] Vector3 base_position;

    void Awake()
    {
        m_world_text = GetComponent<TextMeshPro>();
       
    }
    void Start()
    {
        transform.localPosition = base_position;

        m_target_cam = GameController.Instance.Camera;

        if (!m_target_hp)
        {
            Debug.LogError("Target HealthModule not found");
        }

        m_target_hp.m_health.OnValueChanged.AddListener( hp =>
        {
            if (hp != m_target_hp.m_max_health)
            {
                var color = m_world_text.color;

                color.a = 1;

                m_world_text.color = color;
                m_world_text.text = hp>0?string.Format(m_output_format, hp): m_death_msg;

                StopAllCoroutines();
                StartCoroutine(Misc.ProgressTimer(2f, (progress,delta) =>
                    {
                        color.a = 1f - progress; m_world_text.color = color;
                        transform.localPosition += Vector3.up * delta;
                    },
                    () => transform.localPosition = base_position));
            }
        });
    }

    void Update()
    {
        if (m_target_cam)
        {
            transform.LookAt(m_target_cam.transform, Vector3.up);
        }
    }
}
